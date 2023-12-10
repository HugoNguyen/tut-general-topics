import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { AsyncSubject, BehaviorSubject, Observable, ReplaySubject, Subject, concat, forkJoin, interval, merge, noop, of, timer } from 'rxjs';
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
    // this.sample08_Subject();
    // this.sample09_BehaviorSubject();
    // this.sample10_AsyncSubject();
    // this.sample11_ReplaySubject();
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

  /**
   * Output:
   *  early sub:1
   *  early sub:2
   *  early sub:3
   *  early sub: 4 // after 3s
   *  late sub: 4
   * Explanation:
   * - Subject is both Observable and Observer
   * - Plan Subject does not support late subscription
   * - Values emitted before the second subscription established will not be shown
   * - After 3s, new value emitted, and both subscription will receive value
   */
  sample08_Subject() {
    const subject = new Subject();

    const series$ = subject.asObservable();
    series$.subscribe(val => console.log(`early sub:` + val));
    
    subject.next(1);
    subject.next(2);
    subject.next(3);
    // subject.complete();

    setTimeout(() => {
      series$.subscribe(val => console.log('late sub:' + val));
      subject.next(4);
    }, 3000);
    
  }

  /**
   * Output:
   *  early sub:0
   *  early sub:1
   *  early sub:2
   *  early sub:3
   *  late sub: 3 // after 3s
   * Explanation:
   * - BehaviorSubject emits its current value whenever it is subscribed to.
   * - After 3s, the second subscription established, it will receive the last value of subject
   * Note:
   * - if subject completed before the second subscription established.
   *    It will not receive the last value of subject
   */
  sample09_BehaviorSubject() {
    const subject = new BehaviorSubject(0);

    const series$ = subject.asObservable();
    series$.subscribe(val => console.log(`early sub:` + val));
    
    subject.next(1);
    subject.next(2);
    subject.next(3);
    // subject.complete(); // late sub: 3 will not show

    setTimeout(() => {
      series$.subscribe(val => console.log('late sub:' + val));
    }, 3000);
  }

  /**
   * Output:
   *  first sub:3
   *  second sub:3 // after 3s
   * Explanation:
   *  - AsyncSubject will emit its latest value to all its observers on completion.
   */
  sample10_AsyncSubject() {
    const subject = new AsyncSubject();

    const series$ = subject.asObservable();
    series$.subscribe(val => console.log(`first sub:` + val));
    
    subject.next(1);
    subject.next(2);
    subject.next(3);
    subject.complete(); // If subject does not complete, it will not emit value.

    setTimeout(() => {
      series$.subscribe(val => console.log('second sub:' + val));
    }, 3000);
  }

  /**
   * Output:
   *  first sub:1
   *  first sub:2
   *  first sub:3
   *  second sub:1 // after 3s
   *  second sub:2
   *  second sub:3
   *  first sub:4
   *  second sub:3
   * Explanation:
   *  - ReplaySubject will "replays" old values
   *      to new subscribers by emitting them when they first subscribe.
   */
  sample11_ReplaySubject() {
    const subject = new ReplaySubject();

    const series$ = subject.asObservable();
    series$.subscribe(val => console.log(`first sub:` + val));
    
    subject.next(1);
    subject.next(2);
    subject.next(3);

    setTimeout(() => {
      series$.subscribe(val => console.log('second sub:' + val));
      subject.next(4);
    }, 3000);
  }
}
