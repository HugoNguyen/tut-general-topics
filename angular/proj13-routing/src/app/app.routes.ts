import { Routes } from "@angular/router";

import { routes as usersRoutes } from './users/users.routes';
import { NoTaskComponent } from "./tasks/no-task/no-task.component";
import { resolveTitle, resolveUserName, UserTasksComponent } from "./users/user-tasks/user-tasks.component";
import { NotFoundComponent } from "./not-found/not-found.component";

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