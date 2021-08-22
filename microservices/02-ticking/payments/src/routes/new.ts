import express, { Request, Response } from 'express';
import { body } from 'express-validator';
import { requireAuth, validateRequest, BadRequestError, NotFoundError, NotAuthorizedError } from '@hugo-dev-vn/common';
import { Order, OrderStatus } from '../models/order';
import { Payment } from '../models/payment';
import { stripe } from '../stripe';

const route = express.Router();

route.post('/api/payments',
    requireAuth, [
        body('token')
            .not()
            .isEmpty(),
        body('orderId')
            .not()
            .isEmpty(),
    ],
    validateRequest,
    async (req: Request, res: Response) => {
        const { token, orderId } = req.body;

        const order = await Order.findById(orderId);

        if (!order) {
            throw new NotFoundError();
        }

        if (order.userId !== req.currentUser!.id) {
            throw new NotAuthorizedError();
        }

        if (order.status === OrderStatus.Cancelled) {
            throw new BadRequestError('Cannot pay for an cancelled order');
        }

        const charge = await stripe.charges.create({
            currency: 'usd',
            amount: order.price * 100, // transform to cent
            source: token,
        });
        const payment = Payment.build({
            orderId,
            stripeId: charge.id,
        });
        await payment.save();

        res.status(201).send({ success: true });
    }
);

export { route as createChargeRouter };