import { Msg } from "nats";
import { io } from '../../app';
import { Subjects, LongProcessingCopmletedEvent, Listener } from '../../commons';
import { store } from "../../store";
import { queueGroupName } from './queue-group-name';

export class LongProcessingCopmletedListener extends Listener<LongProcessingCopmletedEvent> {

    readonly subject = Subjects.LongProcessingCompleted;
    queueGroupName = queueGroupName;

    async onMessage(data: LongProcessingCopmletedEvent['data'], msg: Msg): Promise<void> {
        if(!data) return;
        if(!data.id || !data.connectionId || !data.createdAt || !data.completedAt) {
            console.error(`invalid data`);
            return;
        };

        const updatedItem = store.updateItem(data.id, data);
        
        console.log('completed at', updatedItem);
        
        io.to(data.connectionId).emit('long-processing-completed', data);
    }
}
