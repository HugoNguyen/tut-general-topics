import { Injectable } from "@angular/core";
import { BehaviorSubject, Observable, Subject, of } from "rxjs";
import { Course } from "../model/course";
import { createHttpObservable } from "./util";
import { debounceTime, filter, map, tap } from "rxjs/operators";
import { fromPromise } from "rxjs/internal-compatibility";

@Injectable({
    providedIn: 'root'
})
export class Store {
    
    private subject = new BehaviorSubject<Course[]>([]);
    
    courses$: Observable<Course[]> = this.subject.asObservable();

    init() {
        const http$ = createHttpObservable<Course[]>('/api/courses');

        http$
          .pipe(
            tap(() => console.log(`HTTP request executed`)),
            map(res => res['payload']),
          )
          .subscribe(courses => this.subject.next(courses));
    }

    selectBeginnerCourses(): Observable<Course[]> {
        return this.filterByCategory('BEGINNER');
    }

    selectAdvancedCourses(): Observable<Course[]> {
        return this.filterByCategory('ADVANCED');
    }

    selectCourseById(courseId: number) {
        return this.courses$
            .pipe(
                map(courses => courses.find(course => course.id == courseId)),
                filter(course => !!course)
            );
    }

    filterByCategory(category: string) {
        return this.courses$
            .pipe(
                map(courses => courses.filter(course => course.category == category))
            );
    }

    saveCourse(courseId: number, changes) {

        const courses = this.subject.getValue();

        const courseIndex = courses.findIndex(course => course.id == courseId);

        const newCourses = courses.slice(0);

        newCourses[courseIndex] = {
            ...courses[courseIndex],
            ...changes,
        };

        return fromPromise(fetch(`/api/courses/${courseId}`, {
            method: 'PUT',
            body: JSON.stringify(changes),
            headers: {
                'content-type': 'application/json'
            }
        }))
        .pipe(
            tap(_ => this.subject.next(newCourses)),
        );
    }
}