import { Message, Stan } from 'node-nats-streaming';
import { Subjects } from './subjects';

interface Event {
    subject: Subjects;
    data: any;
}

/**
 * T extends from Event
 */
export abstract class Listener<T extends Event> {
    
    // subject must be extracly equal to whatever subject was provided on T
    abstract subject: T['subject'];
    abstract queueGroupName: string;

    // data must be extracly equal to whatever data was provided on T
    abstract onMessage(data: T['data'], msg: Message): void;
    protected client: Stan;
    protected ackWait = 5* 1000;

    constructor(client: Stan) {
        this.client = client;
    }

    subscriptionOptions() {
        return this.client
            .subscriptionOptions()
            .setDeliverAllAvailable() // First time subscription created , all message be emitted
            .setManualAckMode(true)
            .setAckWait(this.ackWait)
            .setDurableName(this.queueGroupName);
    }

    listen() {
        const subscription = this.client.subscribe(
            this.subject,
            this.queueGroupName,
            this.subscriptionOptions()
        );

        subscription.on('message', (msg: Message) => {
            console.log(
                `Message received: ${this.subject} / ${this.queueGroupName}`
            );

            const parseData = this.parseMessage(msg);
            this.onMessage(parseData, msg);
        });
    }

    parseMessage(msg: Message) {
        const data = msg.getData();
        return typeof data === 'string'
            ? JSON.parse(data)
            : JSON.parse(data.toString('utf-8'));
    }
}
