import { Subjects } from "./subjects";

export interface TickedUpdatedEvent {
    subject: Subjects.TicketUpdated;
    data: {
        id: string;
        title: string;
        price: number;
        userId: string;
    };
}
