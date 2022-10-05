import { NatsConnection, SubscriptionOptions, StringCodec, Msg } from 'nats';
import { Subjects } from '../subjects';

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
    abstract onMessage(data: T['data'], msg: Msg): void;
    protected client: NatsConnection;
    protected ackWait = 5 * 1000;
    protected options?: SubscriptionOptions;

    constructor(client: NatsConnection) {
        this.client = client;
        this.setDefaultOption();
    }

    setDefaultOption(options?: SubscriptionOptions) {
        const defaultOption: SubscriptionOptions = {
            queue: this.queueGroupName,
        };

        this.options = {
            ...defaultOption,
            ...options
        };
    }

    async listen() {
        const subscription = this.client.subscribe(
            this.subject,
            this.options,
        );

        // create a codec
        const sc = StringCodec();
        console.log(`listening for ${subscription.getSubject()} requests...`);

        for await (const m of subscription) {
            console.log(`Message received: ${this.subject} / ${this.queueGroupName} / number: [${subscription.getProcessed()}]`);
            
            const parseData = sc.decode(m.data);

            this.onMessage(JSON.parse(parseData), m);
        }

        console.log(`subscription ${subscription.getSubject()} drained.`);
    }
}