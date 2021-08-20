import { Message } from "node-nats-streaming";
import { Subjects, Listener, TickedCreatedEvent } from "@hugo-dev-vn/common";
import { queueGroupName } from "./queue-group-name";
import { Ticket } from "../../models/ticket";

export class TicketCreatedListener extends Listener<TickedCreatedEvent> {

    readonly subject = Subjects.TicketCreated;
    queueGroupName = queueGroupName;

    async onMessage(data: TickedCreatedEvent['data'], msg: Message): Promise<void> {
        try{
            const { id, title, price } = data;
            const ticket = Ticket.build({
                id, title, price
            });
            await ticket.save();
    
            msg.ack();
        }catch (ex) {
            console.error(ex);
        }
        
    }
}
