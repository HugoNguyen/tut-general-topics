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


@Component({
    selector: 'course',
    templateUrl: './course.component.html',
    styleUrls: ['./course.component.css']
})
export class CourseComponent implements OnInit, AfterViewInit {


    course$: Observable<Course>;
    lessons$: Observable<Lesson[]>;


    @ViewChild('searchInput', { static: true }) input: ElementRef;

    constructor(private route: ActivatedRoute) {


    }

    ngOnInit() {

        const courseId = this.route.snapshot.params['id'];

        this.course$ = createHttpObservable<Course>(`/api/courses/${courseId}`);

        this.lessons$ = createHttpObservable<Lesson[]>(`/api/lessons?courseId=${courseId}&pageSize=100`)
            .pipe(
                map(res => res['payload'])
            );
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
         */
        fromEvent(this.input.nativeElement, 'keyup')
            .pipe(
                map((event: any) => event.target.value),
                debounceTime(400),
                distinctUntilChanged(),
            )
            .subscribe(console.log);


    }




}
