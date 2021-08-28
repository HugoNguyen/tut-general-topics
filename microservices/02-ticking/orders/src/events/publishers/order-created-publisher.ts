import { Publisher, OrderCreatedEvent, Subjects } from "@hugo-dev-vn/common";

export class OrderCreatedPublisher extends Publisher<OrderCreatedEvent> {
    readonly subject = Subjects.OrderCreated;
}
