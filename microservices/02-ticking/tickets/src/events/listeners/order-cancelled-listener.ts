import { Message } from "node-nats-streaming";
import { Subjects, Listener, OrderCancelledEvent } from "@hugo-dev-vn/common";
import { queueGroupName } from './queue-group-name';
import { Ticket } from "../../models/ticket";
import { TicketUpdatedPublisher } from "../publishers/ticket-updated-publisher";

export class OrderCancelledListener extends Listener<OrderCancelledEvent> {

    readonly subject = Subjects.OrderCancelled;
    queueGroupName = queueGroupName;

    async onMessage(data: OrderCancelledEvent['data'], msg: Message): Promise<void> {
        // Find the ticket that the order is reserving
        const ticket = await Ticket.findById(data.ticket.id);
                
        // If no ticket, throw error
        if (!ticket) {
            throw new Error('Ticket not found');
        }

        // Mark the ticket not been reserved
        ticket.set({ orderId: undefined });

        // Save the ticket
        await ticket.save();

        // Published ticket update event
        await new TicketUpdatedPublisher(this.client).publish({
            id: ticket.id,
            version: ticket.version,
            title: ticket.title,
            price: ticket.price,
            userId: ticket.userId,
            orderId: ticket.orderId
        });

        // ack the message
        msg.ack();
    }
}
