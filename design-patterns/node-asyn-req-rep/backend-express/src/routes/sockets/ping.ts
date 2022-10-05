import express, { Request, Response } from 'express';
import { io } from '../../app';

const router = express.Router();

const handleFn = (req: Request, res: Response, next: any) => {
    next();
}

router.get('/api/sockets/ping', handleFn, (req: Request, res: Response) => {
    const s = req.query['s'];

    console.log(`[MESSAGE]: ${s}`);

    io.emit("ping", s);
    
    return res.send({
        message: s
    });
});

export { router as pingSocketRouter };