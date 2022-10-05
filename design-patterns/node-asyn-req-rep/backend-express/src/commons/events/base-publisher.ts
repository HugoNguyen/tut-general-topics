import { NatsConnection, StringCodec, PublishOptions } from 'nats';
import { Subjects } from '../subjects';

interface Event {
    subject: Subjects;
    data: any;
}

export abstract class Publisher<T extends Event> {
    abstract subject: T['subject'];
    protected client: NatsConnection;

    constructor(client: NatsConnection) {
        this.client = client;
    }

    publish(data: Partial<T['data']>, options?: PublishOptions ) {
        // create a codec
        const sc = StringCodec();
        const dataEncoded = sc.encode(JSON.stringify(data));

        this.client.publish(this.subject, dataEncoded, options);
    }
}