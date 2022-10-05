import { server } from './app';
import { LongProcessingCopmletedListener } from './events/listeners/long-processing-completed-listener';
import { natsWrapper } from './nats-wrapper';

const start = async () => {
    const port = process.env.PORT || 80;

    // if (!process.env.NATS_CLIENT_ID) {
    //     throw new Error('NATS_CLIENT_ID must be defined');
    // }

    if (!process.env.NATS_CLUSTER_ID) {
        throw new Error('NATS_CLUSTER_ID must be defined');
    }

    if (!process.env.NATS_URL) {
        throw new Error('NATS_URL must be defined');
    }

    try {
        await natsWrapper.connect(process.env.NATS_CLUSTER_ID, process.env.NATS_URL);

        natsWrapper.client.closed()
            .then(err => {
                if (err) {
                    console.error(`service exited because of error: ${err.message}`);
                    return;
                }

                console.log(`Disconnected to NATS`);
            });

        process.on('SIGINT', () => natsWrapper.client.drain());
        process.on('SIGTERM', () => natsWrapper.client.drain());

        new LongProcessingCopmletedListener(natsWrapper.client).listen();
    } catch (err) {
        console.error(err);
    }

    server.listen(port, () => {
        console.log(`Listening on port ${port}!!!!!!!`);
    });
};

start();