import { Injectable } from "@angular/core";
import { Actions, createEffect, ofType } from "@ngrx/effects";
import { tap } from "rxjs";

import { decrement, increment } from "./counter.actions";


@Injectable()
export class CounterEffects {
    saveCount = createEffect(() => this.actions$.pipe(
        ofType(increment, decrement), // filter only increment and decrement actions
        tap((action) => {
            console.log(action);
            localStorage.setItem('count', action.value.toString());
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

    constructor(private actions$: Actions) {}
}

function Effect(): (target: CounterEffects, propertyKey: "saveCount") => void {
    throw new Error("Function not implemented.");
}
