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
    concatAll, shareReplay, first, take
} from 'rxjs/operators';
import {merge, fromEvent, Observable, concat, forkJoin} from 'rxjs';
import {Lesson} from '../model/lesson';
import {createHttpObservable} from '../common/util';
import { Store } from '../common/store.service';


@Component({
    selector: 'course-v2',
    templateUrl: './course.component.html',
    styleUrls: ['./course.component.css']
})
export class CourseComponent implements OnInit, AfterViewInit {

    courseId:number;

    course$ : Observable<Course>;

    lessons$: Observable<Lesson[]>;


    @ViewChild('searchInput', { static: true }) input: ElementRef;

    constructor(private route: ActivatedRoute, private store: Store) {

    }

    ngOnInit() {

        this.courseId = this.route.snapshot.params['id'];

        this.course$ = this.store.selectCourseById(this.courseId)
            .pipe(
                // first(),
                take(1)
            );

        /**
         * Because course$ is a long running observable
         * That why we must force course$ end with first() operator
         * If not loadLessons will never be called
         */
        forkJoin([this.course$, this.loadLessons()])
            .subscribe(console.log);
    }

    ngAfterViewInit() {
        const searchLessons$ =  fromEvent<any>(this.input.nativeElement, 'keyup')
            .pipe(
                map(event => event.target.value),
                debounceTime(400),
                distinctUntilChanged(),
                switchMap(search => this.loadLessons(search))
            );

        const initialLessons$ = this.loadLessons();

        this.lessons$ = concat(initialLessons$, searchLessons$);

    }

    loadLessons(search = ''): Observable<Lesson[]> {
        return createHttpObservable(
            `/api/lessons?courseId=${this.courseId}&pageSize=100&filter=${search}`)
            .pipe(
                map(res => res["payload"])
            );
    }


}











