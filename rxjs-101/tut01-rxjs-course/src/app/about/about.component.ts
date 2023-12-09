import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { Observable, noop } from 'rxjs';
import { createHttpObservable } from '../common/util';
import { map } from 'rxjs/operators';

@Component({
  selector: 'about',
  templateUrl: './about.component.html',
  styleUrls: ['./about.component.css']
})
export class AboutComponent implements OnInit {

  constructor() { }

  ngOnInit() {

    // this.sample01_BuildHttpObservable();
    // this.sample02_MapOperator();
  }

  sample01_BuildHttpObservable() {

    // deprecated Observable.create
    const http$ = new Observable(observer => {
      fetch(`/api/courses`)
        .then(response => {
          return response.json();
        })
        .then(body => {
          observer.next(body);
          observer.complete();
        })
        .catch(err => {
          observer.error(err);
        })
    });

    http$.subscribe(
      courses => console.log(courses),
      noop, // replace () => {},
      () => console.log('completed')
    )
  }

  sample02_MapOperator() {
    const http$ = createHttpObservable('/api/courses');

    const course$ = http$
      .pipe(
        map(res => res['payload'])
      );

    course$.subscribe(
      courses => console.log(courses),
      noop, // replace () => {},
      () => console.log('completed')
    )
  }
}
