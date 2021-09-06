import { combineReducers } from "redux";

import { allRoomsReducer, roomDetailReducer } from "./roomReducers";

import { authReducer, loadedUserReducer, userReducer, forgotPasswordReducer } from './userReducers';

import {
    checkBookingReducer,
    bookedDatesReducer,
    bookingsReducer,
    bookingDetailsReducer
} from './bookingReducers';

const reducer = combineReducers({
    allRooms: allRoomsReducer,
    roomDetails: roomDetailReducer,
    auth: authReducer,
    user: userReducer,
    loadedUser: loadedUserReducer,
    forgotPassword: forgotPasswordReducer,
    checkBooking: checkBookingReducer,
    bookedDates: bookedDatesReducer,
    bookings: bookingsReducer,
    bookingDetails: bookingDetailsReducer,
});

export default reducer;
