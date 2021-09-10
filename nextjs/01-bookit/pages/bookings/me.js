import React from 'react';
import { getSession } from 'next-auth/client';

import MyBooking from "../../components/booking/MyBooking";
import Layout from "../../components/layout/Layout";

import { wrapper } from '../../redux/store';
import { myBookings } from '../../redux/actions/bookingActions';

const MyBookingPage = () => {
    return (
        <Layout title='My Bookings'>
            <MyBooking />
        </Layout>
    )
}

export const getServerSideProps = wrapper.getServerSideProps(store =>
    async ({ req }) => {
        const session = await getSession({ req });

        if (!session) {
            return {
                redirect: {
                    destination: '/login',
                    permanent: false,
                }
            }
        }
    
        await store.dispatch(myBookings(req.headers.cookie, req));
    }
  );
  

export default MyBookingPage;
