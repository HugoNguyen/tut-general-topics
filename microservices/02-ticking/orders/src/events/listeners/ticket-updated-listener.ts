import { Message } from "node-nats-streaming";
import { Subjects, Listener, TickedUpdatedEvent } from "@hugo-dev-vn/common";
import { queueGroupName } from "./queue-group-name";
import { Ticket } from "../../models/ticket";

export class TicketUpdatedListener extends Listener<TickedUpdatedEvent> {

    readonly subject = Subjects.TicketUpdated;
    queueGroupName = queueGroupName;

    async onMessage(data: TickedUpdatedEvent['data'], msg: Message): Promise<void> {
        const ticket = await Ticket.findById(data.id);

        if (!ticket) {
            throw new Error('Ticket not found');
        }

        const { title, price } = data;
        ticket.set({ title, price });
        await ticket.save();

        msg.ack();
    }
}
