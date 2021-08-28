import { Publisher, Subjects, PaymentCreatedEvent } from "@hugo-dev-vn/common";

export class PaymentCreatedPublisher extends Publisher<PaymentCreatedEvent> {
    readonly subject = Subjects.PaymentCreated;
}
