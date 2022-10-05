import { Msg } from "nats";
import { Subjects, LongProcessingRequestEvent, Listener } from '../../commons';
import { doHeavyTask } from "../../do-heavy-task";
import { natsWrapper } from "../../nats-wrapper";
import { LongProcessingCompletedPublisher } from "../publishers/long-processing-completed-publisher";
import { queueGroupName } from './queue-group-name';

export class LongProcessingRequestListener extends Listener<LongProcessingRequestEvent> {

    readonly subject = Subjects.LongProcessingRequest;
    queueGroupName = queueGroupName;

    async onMessage(data: LongProcessingRequestEvent['data'], msg: Msg): Promise<void> {
        console.log('Begin process', data);

        await doHeavyTask();

        const pub = new LongProcessingCompletedPublisher(natsWrapper.client);
        const message = {
            ...data,
            completedAt: Math.floor(new Date().getTime() / 1000)
        };

        pub.publish(message);
    }
}
