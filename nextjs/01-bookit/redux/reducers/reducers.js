import { combineReducers } from "redux";

import { allRoomsReducer, roomDetailReducer } from "./roomReducers";

import { authReducer, loadedUserReducer, userReducer, forgotPasswordReducer } from './userReducers';

const reducer = combineReducers({
    allRooms: allRoomsReducer,
    roomDetails: roomDetailReducer,
    auth: authReducer,
    user: userReducer,
    loadedUser: loadedUserReducer,
    forgotPassword: forgotPasswordReducer,
});

export default reducer;
