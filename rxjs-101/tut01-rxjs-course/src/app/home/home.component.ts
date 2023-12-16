import {Component, OnInit} from '@angular/core';
import {Course} from "../model/course";
import {interval, noop, Observable, of, throwError, timer} from 'rxjs';
import {catchError, delayWhen, finalize, map, retryWhen, shareReplay, tap} from 'rxjs/operators';
import { createHttpObservable } from '../common/util';


@Component({
    selector: 'home',
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
    
    beginnerCourses$: Observable<Course[]>;
    advancedCourses$: Observable<Course[]>;


    constructor() {}

    ngOnInit() {
        const http$ = createHttpObservable('/api/courses');

        /* Apply shareRelay()
        * Result: 
        *   "HTTP request executed" print 1 time
        *   network api call 1 time
        */
        const course$: Observable<Course[]> = http$
          .pipe(
            tap(() => console.log(`HTTP request executed`)),
            map(res => res['payload']),
            shareReplay(),
            retryWhen(errors => errors.pipe(
                delayWhen(() => timer(2000))
            )),
          );

        this.beginnerCourses$ = course$
            .pipe(
                map(courses => courses.filter(course => course.category == 'BEGINNER'))
            );

        this.advancedCourses$ = course$
            .pipe(
                map(courses => courses.filter(course => course.category == 'ADVANCED'))
            );
    }

}
