import { useEffect, useState } from "react";
import { loadStripe } from '@stripe/stripe-js';
import { Elements } from "@stripe/react-stripe-js";
import CheckoutForm from "../../components/checkout-form";
import useRequest from '../../hooks/use-request';
import Router from 'next/router';

// Make sure to call `loadStripe` outside of a component’s render to avoid
// recreating the `Stripe` object on every render.
const stripePromise = loadStripe(
    'pk_test_51JRB2gEwfPm4lLW0gS8meswU1cwKv5znNODA4HIx9C0i3qrPoMZm2QEC7c028HXejm4mKJSrmyNgyKM5vu87DyQm003LymklTX'
);

const OrderShow = ({ order, currentUser }) => {
    const [timeLeft, setTimeLeft] = useState(0);
    const { doRequest, errors } = useRequest({
        url: '/api/payments',
        method: 'post',
        body: {
            orderId: order.id,
        },
        onSuccess: () => Router.push('/orders'),
    })


    useEffect(() => {
        const findTimeLeft = () => {
            const msLeft = new Date(order.expiresAt) - new Date();
            setTimeLeft(Math.round(msLeft / 1000));
        };

        findTimeLeft();
        const timerId = setInterval(findTimeLeft, 1000);

        return () => {
            clearInterval(timerId);
        };
    }, [order]);

    if (timeLeft < 0) {
        return <div>Order Expired</div>
    }

    return (
        <div>
            <div>Time left to pay: {timeLeft}</div>

            <div style={{ marginTop: '4px', marginBottom: '4px' }}>
                Visa: 4242 4242 4242 4242
                Pos: 11111
            </div>

            <Elements stripe={stripePromise}>
                <CheckoutForm
                    email={currentUser.email}
                    amount={order.ticket.price}
                    onSuccess={({ id }) => doRequest({ token: id })}
                />
            </Elements>

            {errors}
        </div>
    )
};

OrderShow.getInitialProps = async (context, client) => {
    const { orderId } = context.query;
    const { data } = await client.get(`/api/orders/${orderId}`);

    return { order: data };
}

export default OrderShow;
