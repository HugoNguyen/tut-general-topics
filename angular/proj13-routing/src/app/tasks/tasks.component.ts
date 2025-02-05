import { Component, computed, DestroyRef, inject, input, OnInit, signal } from '@angular/core';
import { ActivatedRoute, ResolveFn, RouterLink } from '@angular/router';

import { TaskComponent } from './task/task.component';
import { Task } from './task/task.model';
import { TasksService } from './tasks.service';

@Component({
  selector: 'app-tasks',
  standalone: true,
  templateUrl: './tasks.component.html',
  styleUrl: './tasks.component.css',
  imports: [TaskComponent, RouterLink],
})
// export class TasksComponent implements OnInit {
//   userId = input.required<string>();
//   // Op1: extract query params via inputs
//   // order = input<'asc' | 'desc'>();

//   // Op2: extract query params via observable
//   order = signal<'asc' | 'desc'>('asc');
//   private tasksService = inject(TasksService);
//   userTasks = computed(
//     () =>
//       this.tasksService
//         .allTasks()
//         .filter((task) => task.userId === this.userId())
//         .sort((a, b) => {
//           if (this.order() === 'desc') {
//             return a.id > b.id ? -1 : 1;
//           } else {
//             return a.id > b.id ? 1 : -1;
//           }
//         })
//   );
//   private activatedRoute = inject(ActivatedRoute);
//   private destroyRef = inject(DestroyRef);

//   ngOnInit(): void {
//     // Op2: extract query params via observable
//     const subscription = this.activatedRoute.queryParams.subscribe({
//       next: (params) => this.order.set(params['order'] || 'asc'),
//     });

//     this.destroyRef.onDestroy(() => subscription.unsubscribe());
//   }
// }
export class TasksComponent {
  userTasks = input.required<Task[]>();
  userId = input.required<string>();
  order = input<'asc' | 'desc' | undefined>();
}

export const resolveUserTasks: ResolveFn<Task[]> = (
  activatedRouteSnapshot,
  routerState
) => {
  const order = activatedRouteSnapshot.queryParams['order'];
  const tasksService = inject(TasksService);
  const tasks = tasksService
    .allTasks()
    .filter(
      (task) => task.userId === activatedRouteSnapshot.paramMap.get('userId')
    );

  if (order && order === 'asc') {
    tasks.sort((a, b) => (a.id > b.id ? 1 : -1));
  } else {
    tasks.sort((a, b) => (a.id > b.id ? -1 : 1));
  }

  return tasks.length ? tasks : [];
};