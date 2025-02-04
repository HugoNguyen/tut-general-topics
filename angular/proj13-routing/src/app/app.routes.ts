import { Routes } from "@angular/router";

import { NoTaskComponent } from "./tasks/no-task/no-task.component";
import { UserTasksComponent } from "./users/user-tasks/user-tasks.component";

export const routes: Routes = [
    {
        path: '', // <domain>
        component: NoTaskComponent,
    },
    {
        path: 'users/:userId', // <domain>/users/<uid>
        component: UserTasksComponent,
    },

]