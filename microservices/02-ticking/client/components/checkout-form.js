import React, { useState } from "react";
import {
    CardElement,
    useStripe,
    useElements,
} from '@stripe/react-stripe-js';

const CheckoutForm = ({ email, amount, onSuccess }) => {
    const stripe = useStripe();
    const elements = useElements();

    const [errorMessage, setErrorMessage] = useState(null);

    const handleSubmit = async (event) => {
        event.preventDefault();

        setErrorMessage(null);

        if (elements == null) {
            return;
        }

        const { error, paymentMethod } = await stripe.createPaymentMethod({
            type: 'card',
            card: elements.getElement(CardElement),
            billing_details: {
                email,
                name: email,
            },
        });

        if (error) {
            setErrorMessage(
                <div className="alert alert-danger">
                    <h4>Ooops....</h4>
                    <ul className="my-0">
                        <li>{error.message}</li>
                    </ul>
                </div>
            )
            
            return;
        }
        
        onSuccess(paymentMethod);
    };

    return (
        <form onSubmit={handleSubmit}>
            <CardElement />

            <div style={{ marginTop: '4px', marginBottom: '4px'}}>
                {errorMessage}
            </div>

            <section>
                <button type="submit" disabled={!stripe || !elements}>
                    Checkout ${amount}
                </button>
            </section>
            <style jsx>
                {`
                    section {
                        background: #ffffff;
                        display: flex;
                        flex-direction: column;
                        width: 400px;
                        height: 112px;
                        border-radius: 6px;
                        justify-content: space-between;
                        margin: 4px;
                    }
                    button {
                        height: 36px;
                        background: #556cd6;
                        border-radius: 4px;
                        color: white;
                        border: 0;
                        font-weight: 600;
                        cursor: pointer;
                        transition: all 0.2s ease;
                        box-shadow: 0px 4px 5.5px 0px rgba(0, 0, 0, 0.07);
                    }
                    button:hover {
                        opacity: 0.8;
                    }
                `}
            </style>
        </form>
    );
};

export default CheckoutForm;