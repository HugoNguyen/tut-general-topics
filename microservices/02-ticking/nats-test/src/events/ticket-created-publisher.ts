import { Publisher } from "./base-publisher";
import { TickedCreatedEvent } from "./ticket-created-event";
import { Subjects } from "./subjects";

export class TicketCreatedPublisher extends Publisher<TickedCreatedEvent> {
    readonly subject = Subjects.TicketCreated;
}