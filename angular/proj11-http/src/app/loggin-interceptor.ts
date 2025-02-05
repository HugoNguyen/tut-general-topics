import {
    HttpEvent,
    HttpEventType,
    HttpHandler,
    HttpInterceptor,
    HttpRequest,
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, tap } from 'rxjs';

@Injectable()
export class LoggingInterceptor implements HttpInterceptor {
    intercept(request: HttpRequest<unknown>, handler: HttpHandler): Observable<HttpEvent<any>> {
        console.log('[Outgoing Request]');
        console.log(request);
        return handler.handle(request).pipe(
            tap({
                next: event => {
                    if (event.type === HttpEventType.Response) {
                        console.log('[Incoming Response]');
                        console.log(event.status);
                        console.log(event.body);
                    }
                }
            })
        );
    }
}