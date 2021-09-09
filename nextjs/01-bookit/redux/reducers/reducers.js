import { combineReducers } from "redux";

import { allRoomsReducer, newRoomReducer, roomReducer, roomDetailReducer, newReviewReducer, checkReviewReducer } from "./roomReducers";

import { authReducer, loadedUserReducer, userReducer, forgotPasswordReducer } from './userReducers';

import {
    checkBookingReducer,
    bookedDatesReducer,
    bookingsReducer,
    bookingDetailsReducer
} from './bookingReducers';

const reducer = combineReducers({
    allRooms: allRoomsReducer,
    newRoom: newRoomReducer,
    roomDetails: roomDetailReducer,
    room: roomReducer,
    auth: authReducer,
    user: userReducer,
    loadedUser: loadedUserReducer,
    forgotPassword: forgotPasswordReducer,
    checkBooking: checkBookingReducer,
    bookedDates: bookedDatesReducer,
    bookings: bookingsReducer,
    bookingDetails: bookingDetailsReducer,
    newReview: newReviewReducer,
    checkReview: checkReviewReducer,
});

export default reducer;
