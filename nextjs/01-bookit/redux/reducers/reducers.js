import { combineReducers } from "redux";

import { allRoomsReducer, roomDetailReducer } from "./roomReducers";

const reducer = combineReducers({
    allRooms: allRoomsReducer,
    roomDetails: roomDetailReducer,
});

export default reducer;
