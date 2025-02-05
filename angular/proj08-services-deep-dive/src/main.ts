import { bootstrapApplication } from '@angular/platform-browser';

import { AppComponent } from './app/app.component';
import { InjectionToken } from '@angular/core';
import { TasksService } from './app/tasks/tasks.service';

export const TasksServiceToken = new InjectionToken<TasksService>(
    'tasks-serivce-token'
);

bootstrapApplication(AppComponent, {
    providers: [{ provide: TasksServiceToken, useClass: TasksService }],
}).catch((err) => console.error(err));
