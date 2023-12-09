import {AfterViewInit, Component, ElementRef, Inject, OnInit, ViewChild, ViewEncapsulation} from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from "@angular/material/dialog";
import {Course} from "../model/course";
import {FormBuilder, Validators, FormGroup} from "@angular/forms";
import * as moment from 'moment';
import {fromEvent} from 'rxjs';
import {concatMap, distinctUntilChanged, exhaustMap, filter, mergeMap, tap} from 'rxjs/operators';
import {fromPromise} from 'rxjs/internal-compatibility';

@Component({
    selector: 'course-dialog',
    templateUrl: './course-dialog.component.html',
    styleUrls: ['./course-dialog.component.css']
})
export class CourseDialogComponent implements OnInit, AfterViewInit {

    form: FormGroup;
    course:Course;

    @ViewChild('saveButton', { read: ElementRef, static: true }) saveButton: ElementRef;

    @ViewChild('searchInput', { static: true }) searchInput : ElementRef;

    constructor(
        private fb: FormBuilder,
        private dialogRef: MatDialogRef<CourseDialogComponent>,
        @Inject(MAT_DIALOG_DATA) course:Course ) {

        this.course = course;

        this.form = fb.group({
            description: [course.description, Validators.required],
            category: [course.category, Validators.required],
            releasedAt: [moment(), Validators.required],
            longDescription: [course.longDescription,Validators.required]
        });

    }

    ngOnInit() {

        /**
         * Explanation:
         *  Filter values that is valid
         *  Each valid value will be save
         *  New changes will wait previous changes saved before excuting new request for new changes
         *  Problem:
         *      if there're many changes emitted nearly same time (text input).
         *          it will cause lag because of many save requests are queued to excuted
         */
        this.form.valueChanges
            .pipe(
                filter(() => this.form.valid),
                concatMap(changes => this.saveCourse(changes))
            )
            .subscribe();
    }

    saveCourse(changes) {
        return fromPromise(fetch(`/api/courses/${this.course.id}`, {
            method: 'PUT',
            body: JSON.stringify(changes),
            headers: {
                'content-type': 'application/json'
            }
        }));
    }


    ngAfterViewInit() {
        /**
         * Implement Save
         * - Click Save btn and send save request
         * - Handle issue multiple click
         * Explanation:
         * - concatMap cannot fix issue multiple click.
         *      Event emited will create new request
         * - exhaustMap can fix issue multiple click
         *      After an event emitted, a request is created.
         *      Until that request completed, the ongoing events will be ignore
         */
        fromEvent(this.saveButton.nativeElement, 'click')
            .pipe(
                // concatMap(() => this.saveCourse(this.form.value))
                exhaustMap(() => this.saveCourse(this.form.value))
            )
            .subscribe();

    }



    close() {
        this.dialogRef.close();
    }

    save() {}
}
