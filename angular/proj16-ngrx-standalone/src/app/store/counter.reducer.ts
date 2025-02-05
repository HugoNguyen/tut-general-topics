import { createReducer, on } from "@ngrx/store";
import { increment } from "./counter.actions";

const initialState = 0;

// Op1: use ulity fn createReducer
export const counterReducer = createReducer(
    initialState,
    on(increment, (state, action) => state + action.value),
);

// Op2: not use ulity fn createReducer
// export function counterReducer(state = initialState, action: any) {
//     return state;
// }