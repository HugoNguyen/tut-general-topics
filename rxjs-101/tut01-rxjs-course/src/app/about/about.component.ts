import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { Observable, concat, forkJoin, interval, merge, noop, of, timer } from 'rxjs';
import { createHttpObservable } from '../common/util';
import { map, take } from 'rxjs/operators';
import { RxJsLoggingLevel, debug, setRxjsLoggingLevel } from '../common/debug';

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
    // this.sample04_Merge();
    // this.sample05_Unsubscribe();
    // this.sample06_Debug();
    // this.sample07_ForkJoin();
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

  /**
   * Output
   *  0 0 1 10 2 20 3 30
   * Explanation:
   *  Flatten output from source 1 and source 2 into 1 output
   *  source 1 and 2 emit valua parallel
   */
  sample04_Merge() {
    const interval1$ = interval(1000);
    const interval2$ = interval1$.pipe(map(vl => 10 * vl));
    const result$ = merge(interval1$, interval2$);
    result$.subscribe(console.log);
  }

  /**
   * Example unsubscribe and cancel request
   */
  sample05_Unsubscribe() {
    const http$ = createHttpObservable('/api/courses');
    const sub = http$.subscribe(console.log);
    setTimeout(() => sub.unsubscribe(), 0);
  }

  sample06_Debug() {
    setRxjsLoggingLevel(RxJsLoggingLevel.INFO);

    createHttpObservable('/api/courses')
      .pipe(
        debug(RxJsLoggingLevel.INFO, 'courses'),
      )
      .subscribe();
  }

  sample07_ForkJoin() {
    const observable = forkJoin([
      of(1, 2, 3, 4),
      Promise.resolve(8),
      timer(4000)
    ]);
    observable.subscribe({
     next: value => console.log(value),
     complete: () => console.log('This is how it ends!'),
    });
     
    // Logs:
    // [4, 8, 0] after 4 seconds
    // 'This is how it ends!' immediately after
  }
}
