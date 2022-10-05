import express, { Request, Response } from 'express';
import { store } from '../../store';

const router = express.Router();

const handleFn = (req: Request, res: Response, next: any) => {
    next();
}

router.get('/api/long-services', handleFn, (req: Request, res: Response) => {
    res.send(store.events);
});

export { router as indexLongServicesRouter };