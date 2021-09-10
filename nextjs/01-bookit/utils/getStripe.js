import { loadStripe } from '@stripe/stripe-js';
import getConfig from "next/config";

let stripePromise;

const getStripe = () => {
    const { serverRuntimeConfig: { STRIPE_API_KEY }, publicRuntimeConfig } = getConfig();

    if (!stripePromise) {
        stripePromise = loadStripe(STRIPE_API_KEY || publicRuntimeConfig.STRIPE_API_KEY);
    }

    return stripePromise;
}

export default getStripe;
