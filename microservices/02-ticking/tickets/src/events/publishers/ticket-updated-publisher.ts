import { Publisher, Subjects, TickedUpdatedEvent } from "@hugo-dev-vn/common";

export class TicketUpdatedPublisher extends Publisher<TickedUpdatedEvent> {
    readonly subject = Subjects.TicketUpdated;
}