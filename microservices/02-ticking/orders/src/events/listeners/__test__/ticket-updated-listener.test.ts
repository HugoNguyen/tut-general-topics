import mongoose from 'mongoose';
import { Message } from 'node-nats-streaming';
import { TicketUpdatedEvent } from '@hugo-dev-vn/common';
import { TicketUpdatedListener } from "../ticket-updated-listener";
import { natsWrapper } from "../../../nats-wrapper";
import { Ticket } from '../../../models/ticket';

const setup = async () => {
    // create an instance of the listener
    const listener = new TicketUpdatedListener(natsWrapper.client);

    // Create and save a ticket
    const ticket = Ticket.build({
        id: mongoose.Types.ObjectId().toHexString(),
        title: 'concert',
        price: 20,
    });
    await ticket.save();

    // create a fake data event
    const data: TicketUpdatedEvent['data'] = {
        id: ticket.id,
        version: ticket.version + 1,
        title: 'new concert',
        price: 999,
        userId: 'asdfsdf',
    }

    // create a fake message object
    // @ts-ignore
    const msg: Message = {
        ack: jest.fn(),
    };

    return { listener, data, msg, ticket };
};

it('finds, updates, and saves a ticket', async () => {
    const { listener, data, msg, ticket } = await setup();

    // call the onMessage fun with the data obj + message obj
    await listener.onMessage(data, msg);

    // write assertions to make sure a ticket was created!
    const updateTicket = await Ticket.findById(ticket.id);

    expect(updateTicket!.title).toEqual(data.title);
    expect(updateTicket!.price).toEqual(data.price);
    expect(updateTicket!.version).toEqual(data.version);
});

it('acks the message', async () => {
    const { msg, data, listener } = await setup();

    await listener.onMessage(data, msg);

    expect(msg.ack).toHaveBeenCalled();
});

it('does not call ack if the event has a skipped version number',
    async () => {
        const { msg, data, listener } = await setup();
        data.version = 10;

        try {
            await listener.onMessage(data, msg);
        } catch(err) {

        }
        
        expect(msg.ack).not.toHaveBeenCalled();
    });