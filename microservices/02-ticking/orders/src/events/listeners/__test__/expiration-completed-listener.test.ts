import mongoose from 'mongoose';
import { Message } from 'node-nats-streaming';
import { ExpirationCompletedEvent } from '@hugo-dev-vn/common';
import { ExpirationCompletedListener } from "../expiration-completed-listener";
import { natsWrapper } from "../../../nats-wrapper";
import { Order, OrderStatus } from '../../../models/order';
import { Ticket } from '../../../models/ticket';

const setup = async () => {
    const listener = new ExpirationCompletedListener(natsWrapper.client);

    const ticket = Ticket.build({
        id: mongoose.Types.ObjectId().toHexString(),
        title: 'concert',
        price: 20
    });
    await ticket.save();

    const order = Order.build({
        status: OrderStatus.Created,
        userId: mongoose.Types.ObjectId().toHexString(),
        expiresAt: new Date(),
        ticket,
    });
    await order.save();

    // create a fake data event
    const data: ExpirationCompletedEvent['data'] = {
        orderId: order.id,
    }

    // create a fake message object
    // @ts-ignore
    const msg: Message = {
        ack: jest.fn(),
    };

    return { listener, data, msg, ticket, order };
}

it('updates the order status to cancelled', async () => {
    const { listener, data, msg, order } = await setup();

    await listener.onMessage(data, msg);

    const updateOrder = await Order.findById(order.id);

    expect(updateOrder!.status).toEqual(OrderStatus.Cancelled);
});

it('emit an OrderCancelled event', async () => {
    const { listener, data, msg, order } = await setup();

    await listener.onMessage(data, msg);

    expect(natsWrapper.client.publish).toHaveBeenCalled();

    const eventData = JSON.parse((natsWrapper.client.publish as jest.Mock).mock.calls[0][1]);

    expect(eventData.id).toEqual(order.id);
});

it('ack the message', async () => {
    const { listener, data, msg } = await setup();

    await listener.onMessage(data, msg);

    expect(msg.ack).toHaveBeenCalled();
});