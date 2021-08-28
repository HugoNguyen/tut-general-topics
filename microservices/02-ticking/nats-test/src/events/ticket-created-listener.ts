import { Message } from "node-nats-streaming";
import { Listener } from "./base-listener";
import { TickedCreatedEvent } from "./ticket-created-event";
import { Subjects } from "./subjects";

export class TicketCreatedListener extends Listener<TickedCreatedEvent> {
    // Makes sure that we can never change the value of subject to anything else
    // changed from => subject: Subjects.TicketCreated = Subjects.TicketCreated;
    // to using readonly
    readonly subject = Subjects.TicketCreated;

    queueGroupName = 'payments-service';

    // Enforce typescripe type checking on the properties we try to access
    onMessage(data: TickedCreatedEvent['data'], msg: Message) {
        console.log('Event data!', data);
        console.log(data.id);
        console.log(data.title);
        console.log(data.price);
        msg.ack();
    }
}
