import { combineReducers } from "redux";

import { allRoomsReducer, roomDetailReducer } from "./roomReducers";

import { authReducer } from './userReducers';

const reducer = combineReducers({
    allRooms: allRoomsReducer,
    roomDetails: roomDetailReducer,
    auth: authReducer,
});

export default reducer;
