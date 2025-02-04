import { Routes } from "@angular/router";

import { TasksComponent } from "./tasks/tasks.component";
import { NoTaskComponent } from "./tasks/no-task/no-task.component";

export const routes: Routes = [
    {
        path: '', // <domain>
        component: NoTaskComponent,
    },
    {
        path: 'tasks', // <domain>/tasks
        component: TasksComponent,
    }
]