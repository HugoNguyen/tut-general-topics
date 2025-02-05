import { Routes } from "@angular/router";

import { TasksComponent, resolveUserTasks  } from "../tasks/tasks.component";
import { canLeaveEditPage, NewTaskComponent } from "../tasks/new-task/new-task.component";

export const routes: Routes = [
    {
        path: '', // <domain>/users/<uid>
        redirectTo: 'tasks',
        pathMatch: 'prefix',
    },
    {
        path: 'tasks', // <domain>/users/<uid>/tasks
        component: TasksComponent,
        runGuardsAndResolvers: 'paramsOrQueryParamsChange',
        resolve: {
            userTasks: resolveUserTasks,
        },
    },
    {
        path: 'tasks/new', // <domain>/users/<uid>/tasks/new
        component: NewTaskComponent,
        canDeactivate: [canLeaveEditPage],
    }
]