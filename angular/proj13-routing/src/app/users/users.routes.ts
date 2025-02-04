import { Routes } from "@angular/router";

import { TasksComponent } from "../tasks/tasks.component";
import { NewTaskComponent } from "../tasks/new-task/new-task.component";

export const routes: Routes = [
    {
        path: '', // <domain>/users/<uid>
        redirectTo: 'tasks',
        pathMatch: 'prefix',
    },
    {
        path: 'tasks', // <domain>/users/<uid>/tasks
        component: TasksComponent,
    },
    {
        path: 'tasks/new', // <domain>/users/<uid>/tasks/new
        component: NewTaskComponent,
    }
]