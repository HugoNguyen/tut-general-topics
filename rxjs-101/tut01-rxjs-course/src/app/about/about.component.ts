import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { Observable, concat, interval, noop, of } from 'rxjs';
import { createHttpObservable } from '../common/util';
import { map, take } from 'rxjs/operators';

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
    // this.sample03_Concatenation01();
    // this.sample03_Concatenation02();
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

  /**
   * Output
   *  1 2 3 4 5 6 7 8 9
   * Explanation:
   *  source1$ will emit 1, 2, 3 and complete.
   *  Then source2$ begin emit its values then complete
   *  Then source3$ begin emit its values
   */
  sample03_Concatenation01() {
    const source1$ = of(1, 2, 3);

    const source2$ = of(4, 5, 6);

    const source3$ = of(7, 8, 9);

    const result$ = concat(source1$, source2$, source3$);

    result$.subscribe(console.log);
  }

  /**
   * Output
   *  1 2 3 4 5 6 7 8 9
   * Explanation:
   *  source1$ will never completed. It means source2$, source3$ will never be emited
   */
  sample03_Concatenation02() {
    const source1$ = interval(1000);

    const source2$ = of('a', 'b', 'c');

    const source3$ = of('d', 'e', 'f');

    const result$ = concat(source1$, source2$, source3$);

    result$.subscribe(console.log);
  }
}