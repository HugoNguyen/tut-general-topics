import * as nats from 'nats';

class NatsWrapper {
    // Tell ts this variable can be undefine
    private _client?: nats.NatsConnection;

    get client() {
        if (!this._client) {
            throw new Error('Cannot access NATS client before connecting');
        }

        return this._client;
    }

    async connect(clusterId: string, url: string) {
        // this._client = nats.connect(clusterId, clientId, { url });

        // return new Promise((resolve, reject) => {
        //     this.client.on('connect', () => {
        //         console.log('Connected to NATS');
        //         resolve();
        //     });

        //     this.client.on('error', (err) => {
        //         reject(err);
        //     });
        // });

        this._client = await nats.connect({
            servers: url,
        });

        console.log('Connected to NATS');
    }
    
}

export const natsWrapper = new NatsWrapper();