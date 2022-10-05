import express, { Request, Response } from 'express';
import { LongProcessingRequestPublisher } from '../../events/publishers/long-processing-request-publisher';
import { natsWrapper } from '../../nats-wrapper';
import { store } from '../../store';

const router = express.Router();

const handleFn = (req: Request, res: Response, next: any) => {
    next();
}

router.post('/api/long-services', handleFn, (req: Request, res: Response) => {
    const connectionId = req.header('X-CONNECTION-ID');

    if(!connectionId) {
        return res.status(404).send({
            error: 'missing field {connectionid}'
        })
    }

    const pub = new LongProcessingRequestPublisher(natsWrapper.client);

    const newEvent = store.addItem({
        connectionId,
        createdAt: Math.floor(new Date().getTime() / 1000),
    })

    pub.publish(newEvent);
    
    res.send({
        name: req.path,
        method: req.method,
        payload: newEvent,
    })
});

export { router as postLongServicesRouter };