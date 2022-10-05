import { Publisher, Subjects, LongProcessingRequestEvent } from "../../commons/index";

export class LongProcessingRequestPublisher extends Publisher<LongProcessingRequestEvent> {
    readonly subject = Subjects.LongProcessingRequest;
}
