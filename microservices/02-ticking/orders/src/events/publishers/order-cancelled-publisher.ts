import { Publisher, OrderCancelledEvent, Subjects } from "@hugo-dev-vn/common";

export class OrderCancelledPublisher extends Publisher<OrderCancelledEvent> {
    readonly subject = Subjects.OrderCancelled;
}
