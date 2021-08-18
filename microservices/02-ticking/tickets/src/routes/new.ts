import express, { Request, Response } from 'express';
import { body } from 'express-validator';
import { requireAuth, validateRequest } from '@hugo-dev-vn/common';
import { Ticket } from '../models/ticket';

const route = express.Router();

route.post('/api/tickets', requireAuth, [
        body('title')
            .not()
            .isEmpty()
            .withMessage('Title is required'),
        body('price')
            .isFloat({ gt: 0 })
            .withMessage('Price must bi greater than 0'),
    ],
    validateRequest,
    async (req: Request, res: Response) => {

    const { title, price } = req.body;

    const ticket = Ticket.build({
        title,
        price,
        userId: req.currentUser!.id
    });
    await ticket.save();

    res.status(201).send(ticket);
});

export { route as createTicketRouter };