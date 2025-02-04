import { CanMatchFn, RedirectCommand, Router, Routes } from "@angular/router";

import { routes as usersRoutes } from './users/users.routes';
import { NoTaskComponent } from "./tasks/no-task/no-task.component";
import { resolveTitle, resolveUserName, UserTasksComponent } from "./users/user-tasks/user-tasks.component";
import { NotFoundComponent } from "./not-found/not-found.component";
import { inject } from "@angular/core";

const dummyCanMatch: CanMatchFn = (route, segments) => {
    const router = inject(Router);
    const shouldGetAccess = Math.random();
    if (shouldGetAccess < 0.5) {
        return true;
    }
    return new RedirectCommand(router.parseUrl('/unauthorized'));
}

export const routes: Routes = [
    {
        path: '', // <domain>
        component: NoTaskComponent,
        title: 'Welcome to Task Management'
    },
    {
        path: 'users/:userId', // <domain>/users/<uid>
        component: UserTasksComponent,
        children: usersRoutes,
        canMatch: [dummyCanMatch],
        data: {
            message: 'Hello!'
        },
        resolve: {
            userName: resolveUserName
        },
        title: resolveTitle,
    },
    {
        path: '**',
        component: NotFoundComponent
    }
]