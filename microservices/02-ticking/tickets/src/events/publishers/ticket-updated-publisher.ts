import { Publisher, Subjects, TicketUpdatedEvent } from "@hugo-dev-vn/common";

export class TicketUpdatedPublisher extends Publisher<TicketUpdatedEvent> {
    readonly subject = Subjects.TicketUpdated;
}