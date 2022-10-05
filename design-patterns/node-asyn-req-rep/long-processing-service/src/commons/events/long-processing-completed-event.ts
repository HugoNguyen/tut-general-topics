import { Subjects } from "../subjects";

export interface LongProcessingCopmletedEvent {
    subject: Subjects.LongProcessingCompleted;
    data: {
        id: string;
        connectionId: string;
        createdAt: number;
        completedAt: number;
    }
}
