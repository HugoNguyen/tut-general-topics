import { createReducer } from "@ngrx/store";

const initialState = 0;

// Op1: use ulity fn createReducer
// export const counterReducer = createReducer(
//     initialState
// );

// Op2: not use ulity fn createReducer
export function counterReducer(state = initialState, action: any) {
    return state;
}