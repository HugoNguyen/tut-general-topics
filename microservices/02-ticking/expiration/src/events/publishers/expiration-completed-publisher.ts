import { Publisher, ExpirationCompletedEvent, Subjects } from "@hugo-dev-vn/common";

export class ExpirationCompletedPublisher extends Publisher<ExpirationCompletedEvent> {
    readonly subject = Subjects.ExpirationCompleted;
}
