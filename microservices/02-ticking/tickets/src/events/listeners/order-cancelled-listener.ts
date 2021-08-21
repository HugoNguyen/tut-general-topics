import { Message } from "node-nats-streaming";
import { Subjects, Listener, OrderCancelledEvent } from "@hugo-dev-vn/common";
import { queueGroupName } from './queue-group-name';

export class OrderCancelledListener extends Listener<OrderCancelledEvent> {

    readonly subject = Subjects.OrderCancelled;
    queueGroupName = queueGroupName;

    async onMessage(data: OrderCancelledEvent['data'], msg: Message): Promise<void> {

    }
}
