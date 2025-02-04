import { Routes } from "@angular/router";

import { routes as usersRoutes } from './users/users.routes';
import { NoTaskComponent } from "./tasks/no-task/no-task.component";
import { UserTasksComponent } from "./users/user-tasks/user-tasks.component";
import { NotFoundComponent } from "./not-found/not-found.component";

export const routes: Routes = [
    {
        path: '', // <domain>
        component: NoTaskComponent,
    },
    {
        path: 'users/:userId', // <domain>/users/<uid>
        component: UserTasksComponent,
        children: usersRoutes,
    },
    {
        path: '**',
        component: NotFoundComponent
    }
]