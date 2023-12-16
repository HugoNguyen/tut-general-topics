import { Observable } from "rxjs";


export function createHttpObservable<T>(url: string): Observable<T> {

    // deprecated Observable.create
    return new Observable<T>(observer => {
        const controller = new AbortController();
        const signal = controller.signal;

        fetch(`${url}`, { signal })
            .then(response => {
                if (response.ok) {
                    return response.json();
                } else {
                    observer.error('Request failed with status code: ' + response.status);
                }
            })
            .then(body => {
                observer.next(body);
                observer.complete();
            })
            .catch(err => {
                observer.error(err);
            });

        return () => controller.abort();
    });
}