import { Publisher, Subjects, TickedCreatedEvent } from "@hugo-dev-vn/common";

export class TicketCreatedPublisher extends Publisher<TickedCreatedEvent> {
    readonly subject = Subjects.TicketCreated;
}