import { Injectable } from "@angular/core";
import { Actions, createEffect, ofType } from "@ngrx/effects";
import { Store } from "@ngrx/store";
import { tap, withLatestFrom } from "rxjs";

import { decrement, increment } from "./counter.actions";
import { selectCount } from "./counter.selectors";



@Injectable()
export class CounterEffects {
    saveCount = createEffect(() => this.actions$.pipe(
        ofType(increment, decrement), // filter only increment and decrement actions
        withLatestFrom(this.store.select(selectCount)), // get value from store
        tap(([action, counter]) => {
            console.log(action);
            localStorage.setItem('count', counter.toString());
        }),
    ), { 
        dispatch: false, // not dispatch any actions after complete
    });

    // Old verions, using @Effect
    // @Effect({ dispatch: false })
    // saveCount = this.actions$.pipe(
    //     ofType(increment, decrement),
    //     tap((action) => {
    //         console.log(action);
    //         localStorage.setItem('count', action.value.toString());
    //     }),
    // );

    constructor(private actions$: Actions, private store: Store<{counter: number}>) {}
}

function Effect(): (target: CounterEffects, propertyKey: "saveCount") => void {
    throw new Error("Function not implemented.");
}
