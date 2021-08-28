import { Publisher, Subjects, TicketCreatedEvent } from "@hugo-dev-vn/common";

export class TicketCreatedPublisher extends Publisher<TicketCreatedEvent> {
    readonly subject = Subjects.TicketCreated;
}