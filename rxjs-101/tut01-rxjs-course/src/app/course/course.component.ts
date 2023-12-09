import {AfterViewInit, Component, ElementRef, OnInit, ViewChild} from '@angular/core';
import {ActivatedRoute} from "@angular/router";
import {Course} from "../model/course";
import {
    debounceTime,
    distinctUntilChanged,
    startWith,
    tap,
    delay,
    map,
    concatMap,
    switchMap,
    withLatestFrom,
    concatAll, shareReplay
} from 'rxjs/operators';
import {merge, fromEvent, Observable, concat} from 'rxjs';
import {Lesson} from '../model/lesson';
import { createHttpObservable } from '../common/util';
import { RxJsLoggingLevel, debug } from '../common/debug';


@Component({
    selector: 'course',
    templateUrl: './course.component.html',
    styleUrls: ['./course.component.css']
})
export class CourseComponent implements OnInit, AfterViewInit {

    courseId: string;

    course$: Observable<Course>;
    lessons$: Observable<Lesson[]>;


    @ViewChild('searchInput', { static: true }) input: ElementRef;

    constructor(private route: ActivatedRoute) {


    }

    ngOnInit() {

        this.courseId = this.route.snapshot.params['id'];

        this.course$ = createHttpObservable<Course>(`/api/courses/${this.courseId}`);
    }

    ngAfterViewInit() {

        /**
         * Every time press, send a request to search lesson
         * After an event emmited, wait 400ms before back to receive new event
         * - To not make same request for same value
         * - Event emited while waiting will be ignore
         * - use debounceTime
         * 
         * Problem:
         * - Value emitted from keyup event, may be the same. It is expensive to create request for the same value
         * -- fix with distinctUntilChanged
         * - concatMap: cause lag, because every new value emited, it will produce new request.
         * -- preview request must be unsubscribed (canceled)
         */
        /**
         * First time access this component
         * It should load default all lesson
         * Explanation:
         * - initialLessons$ will run first and completed. The initial list will be load
         * - Then start searchLesson$
         */
        /** 
         * Throttling vs Debouncing
         * - throtting to implement rate limit
         * - debouncing to wait the output stable
         */
        this.lessons$ = fromEvent(this.input.nativeElement, 'keyup')
            .pipe(
                map((event: any) => event.target.value),
                startWith(''),
                debug(RxJsLoggingLevel.INFO, 'search'),
                debounceTime(400),
                // throttleTime(400),
                distinctUntilChanged(),
                // concatMap(search => this.loadLessons(search))
                switchMap(search => this.loadLessons(search))
            );
    }

    loadLessons(search: string = ''): Observable<Lesson[]> {
        return createHttpObservable<Lesson[]>(`/api/lessons?courseId=${this.courseId}&pageSize=100&filter=${search}`)
        .pipe(
            map(res => res['payload'])
        );
    }


}
