import { Routes } from '@angular/router';
import { UsingViewContainerRefComponent } from './example02/using-view-container-ref/using-view-container-ref.component';
import { UsingNgComponentOutletComponent } from './example01/using-ng-component-outlet/using-ng-component-outlet.component';
import { LazyLoadingComponent } from './example03/lazy-loading/lazy-loading.component';

export const routes: Routes = [
    {
        path: 'ng-component-outlet',
        component: UsingNgComponentOutletComponent
    },
    {
        path: 'view-container-ref',
        component: UsingViewContainerRefComponent
    },
    {
        path: 'lazy-loading-components',
        component: LazyLoadingComponent
    }
];
