import React from 'react';
import { useSelector, useDispatch } from 'react-redux';
import { toast } from 'react-toastify';
import { MDBDataTable } from 'mdbreact';
import easyinvoice from 'easyinvoice';
import Link from 'next/link';
import { clearErrors } from '../../redux/actions/bookingActions';

const MyBooking = () => {

    const dispatch = useDispatch();

    const { bookings, error } = useSelector(state => state.bookings);

    React.useEffect(() => {
        if (error) {
            toast.error(error);
            dispatch(clearErrors());
        }
    }, [dispatch, error]);

    const setBookings = () => {
        const data = {
            columns: [
                {
                    label: 'Booking ID',
                    field: 'id',
                    sort: 'asc'
                },
                {
                    label: 'Check In',
                    field: 'checkIn',
                    sort: 'asc'
                },
                {
                    label: 'Check Out',
                    field: 'checkOut',
                    sort: 'asc'
                },
                {
                    label: 'Amount Paid',
                    field: 'amount',
                    sort: 'asc'
                },
                {
                    label: 'Actions',
                    field: 'actions',
                    sort: 'asc'
                }
            ],
            rows: []
        };


        bookings && bookings.forEach(booking => {
            data.rows.push({
                id: booking._id,
                checkIn: new Date(booking.checkInDate).toLocaleString('en-US'),
                checkOut: new Date(booking.checkOutDate).toLocaleString('en-US'),
                amount: `$${booking.amountPaid}`,
                actions: <>
                    <Link href={`/bookings/${booking._id}`} >
                        <a className='btn btn-primary'>
                            <i className='fa fa-eye'></i>
                        </a>
                    </Link>
                    <button className='btn btn-success mx-2' onClick={() => downloadInvoice(booking)}>
                        <i className='fa fa-download'></i>
                    </button>
                </>
            })
        });


        return data;
    }

    const downloadInvoice = async (booking) => {
        const data = {
            "documentTitle": "Booking INVOICE", //Defaults to INVOICE
            //"locale": "de-DE", //Defaults to en-US, used for number formatting (see docs)
            "currency": "USD", //See documentation 'Locales and Currency' for more info
            "taxNotation": "vat", //or gst
            "marginTop": 25,
            "marginRight": 25,
            "marginLeft": 25,
            "marginBottom": 25,
            "logo": "https://res.cloudinary.com/hugo-dev-vn/image/upload/v1630916762/bookit/bookit_logo_cbgjzv_akq1iu.png", //or base64
            // "background": "https://public.easyinvoice.cloud/img/watermark-draft.jpg", //or base64 //img or pdf
            "sender": {
                "company": "Book IT",
                "address": "13 Street",
                "zip": "1234 AB",
                "city": "Sampletown",
                "country": "USA"
                //"custom1": "custom value 1",
                //"custom2": "custom value 2",
                //"custom3": "custom value 3"
            },
            "client": {
                "company": `${booking.user.name}`,
                "address": `${booking.user.email}`,
                "zip": "",
                "city": `Check In: ${new Date(booking.checkInDate).toLocaleString('en-US')}`,
                "country": `Check In: ${new Date(booking.checkOutDate).toLocaleString('en-US')}`
                //"custom1": "custom value 1",
                //"custom2": "custom value 2",
                //"custom3": "custom value 3"
            },
            "invoiceNumber": `${booking._id}`,
            "invoiceDate": `${new Date(Date.now()).toLocaleDateString('en-US')}`,
            "products": [
                {
                    "quantity": `${booking.daysOfStay}`,
                    "description": `${booking.room.name}`,
                    "tax": 0,
                    "price": booking.room.pricePerNight
                },
            ],
            "bottomNotice": "This is auto generated Invoice of your booking on Book IT",
            //Used for translating the headers to your preferred language
            //Defaults to English. Below example is translated to Dutch
            // "translate": { 
            //     "invoiceNumber": "Factuurnummer",
            //     "invoiceDate": "Factuurdatum",
            //     "products": "Producten", 
            //     "quantity": "Aantal", 
            //     "price": "Prijs",
            //     "subtotal": "Subtotaal",
            //     "total": "Totaal" 
            // }
        };

        const result = await easyinvoice.createInvoice(data);
        easyinvoice.download(`invoice_${booking._id}.pdf`, result.pdf);
    }

    return (
        <div className='container container-fluid'>
            <h1 className='my-5'>My Bookings</h1>

            <MDBDataTable
                data={setBookings()}
                className='px-3'
                bordered
                striped
                hover
            />

        </div>
    )
}

export default MyBooking
