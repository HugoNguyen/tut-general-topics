import { Message } from "node-nats-streaming";
import { Subjects, Listener, OrderCreatedEvent } from "@hugo-dev-vn/common";
import { queueGroupName } from './queue-group-name';
import { Ticket } from "../../models/ticket";

export class OrderCreatedListener extends Listener<OrderCreatedEvent> {

    readonly subject = Subjects.OrderCreated;
    queueGroupName = queueGroupName;

    async onMessage(data: OrderCreatedEvent['data'], msg: Message): Promise<void> {
        // Find the ticket that the order is reserving
        const ticket = await Ticket.findById(data.ticket.id);
        
        // If no ticket, throw error
        if (!ticket) {
            throw new Error('Ticket not found');
        }

        // Mark the ticket as beging reserved by setting tis orderId property
        ticket.set({ orderId: data.id });

        // Save the ticket
        await ticket.save();
        // TODO, handle version inscrement
        // Ex: current ticket version is 0
        // After ticket update orderId, the version inscrement by 1, now is 1
        // Then Order cancelled, in order-cancelled-listener,
        // ticket remove orderId, the verion now is 2
        // Then, change the ticket price, the ver now is 3
        // Event UpdateTicket emitted, but TicketUpdatedListner cannot handle
        // becuase versions is now diffent over 1

        // ack the message
        msg.ack();
    }
}
