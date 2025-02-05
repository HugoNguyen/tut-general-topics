import { Action, createAction, props } from "@ngrx/store";

// Op1: use ulity fn createAction
export const increment = createAction(
    '[Counter] Increment',
    props<{value: number}>()
);

export const decrement = createAction(
    '[Counter] Decrement',
    props<{value: number}>()
);

// Op2: not use createAction
// export class IncrementAction implements Action {
//     readonly type = '[Counter] Increment';
    
//     constructor(public value: number) {}
// }

// export const increment = (payload: { value: number}) => new IncrementAction(payload.value);
// export type CounterAction = IncrementAction;