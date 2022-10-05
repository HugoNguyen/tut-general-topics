import { Subjects } from "../subjects";

export interface LongProcessingRequestEvent {
    subject: Subjects.LongProcessingRequest;
    data: {
        id: string;
        createdAt: number;
    }
}
