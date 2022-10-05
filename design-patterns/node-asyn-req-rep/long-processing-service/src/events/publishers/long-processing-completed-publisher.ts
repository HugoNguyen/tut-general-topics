import { Publisher, Subjects, LongProcessingCopmletedEvent } from "../../commons/index";

export class LongProcessingCompletedPublisher extends Publisher<LongProcessingCopmletedEvent> {
    readonly subject = Subjects.LongProcessingCompleted;
}
