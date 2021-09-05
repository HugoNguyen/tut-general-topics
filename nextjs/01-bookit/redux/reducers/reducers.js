import { combineReducers } from "redux";

import { allRoomsReducer, roomDetailReducer } from "./roomReducers";

import { authReducer, userReducer, forgotPasswordReducer } from './userReducers';

const reducer = combineReducers({
    allRooms: allRoomsReducer,
    roomDetails: roomDetailReducer,
    auth: authReducer,
    user: userReducer,
    forgotPassword: forgotPasswordReducer,
});

export default reducer;
