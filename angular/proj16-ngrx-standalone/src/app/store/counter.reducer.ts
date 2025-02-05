import { Action, createReducer, on } from "@ngrx/store";
import { increment, decrement } from "./counter.actions";
// import { CounterAction, IncrementAction } from "./counter.actions";

const initialState = 0;

// Op1: use ulity fn createReducer
export const counterReducer = createReducer(
    initialState,
    on(increment, (state, action) => state + action.value),
    on(decrement, (state, action) => state - action.value),
);

// Op2: not use ulity fn createReducer
// export function counterReducer(state = initialState, action: CounterAction | Action) {
//     if (action.type === '[Counter] Increment') {
//         return state + (action as IncrementAction).value;
//     }
//     return state;
// }