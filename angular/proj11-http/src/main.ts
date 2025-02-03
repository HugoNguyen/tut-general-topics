import { bootstrapApplication } from '@angular/platform-browser';

import { AppComponent } from './app/app.component';
import { HTTP_INTERCEPTORS, HttpEventType, HttpHandlerFn, HttpRequest, provideHttpClient, withInterceptors, withInterceptorsFromDi } from '@angular/common/http';
import { tap } from 'rxjs';
import { LoggingInterceptor } from './app/loggin-interceptor';

function logginInterceptor(request: HttpRequest<unknown>, next: HttpHandlerFn) {
    // const req = request.clone({
    //     headers: request.headers.set('X-DEBUG', 'TESTING')
    // })
    console.log('[Outgoing Request]');
    console.log(request);
    return next(request).pipe(
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

bootstrapApplication(AppComponent, {
    providers: [
        provideHttpClient(
            // withInterceptors([logginInterceptor]),
            withInterceptorsFromDi()
        ),
        { provide: HTTP_INTERCEPTORS, useClass: LoggingInterceptor, multi: true }
    ],
}).catch((err) => console.error(err));
