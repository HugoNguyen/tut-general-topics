import { Component, computed, DestroyRef, inject, input, OnInit, signal } from '@angular/core';
import { ActivatedRoute, ActivatedRouteSnapshot, ResolveFn, RouterLink, RouterOutlet, RouterStateSnapshot } from '@angular/router';

import { UsersService } from '../users.service';

@Component({
  selector: 'app-user-tasks',
  standalone: true,
  imports: [RouterOutlet, RouterLink],
  templateUrl: './user-tasks.component.html',
  styleUrl: './user-tasks.component.css',
})
export class UserTasksComponent {
  // Op1.1: Extracting Dynamic Route Parameters via @Input()
  // @Input({required: true}) userId!: string;

  // Op1.2: Extracting Dynamic Route Parameters via input()
  // userId = input.required<string>();
  
  // Op2: Extracting Dynamic Route Parameters via ActiviatedRoute
  // userId = signal<string>('');
  message = input.required<string>();
  // Extract data from route. Provided by resolve fn
  userName = input.required<string>();

  // private usersService = inject(UsersService);
  // private activatedRoute = inject(ActivatedRoute);
  // private destroyRef = inject(DestroyRef);

  // userName = computed(() => this.usersService.users.find(u => u.id === this.userId())?.name);

  // ngOnInit() {
  //   console.log('Input Data: ' + this.message());
  //   const subscription = this.activatedRoute.paramMap.subscribe({
  //     next: paramMap => {
  //       this.userId.set(paramMap.get('userId') || '');
  //     },
  //   });

  //   this.destroyRef.onDestroy(() => subscription.unsubscribe());
  // }
}

export const resolveUserName: ResolveFn<string> = (
  activatedRoute: ActivatedRouteSnapshot,
  routerState: RouterStateSnapshot
) => {
  const usersService = inject(UsersService);
  const userName = usersService
    .users
    .find(u => u.id === activatedRoute.paramMap.get('userId'))?.name || '';
  return userName;
}

export const resolveTitle: ResolveFn<string> = (
  activatedRoute: ActivatedRouteSnapshot,
  routerState: RouterStateSnapshot
) => {
  return resolveUserName(activatedRoute, routerState) + '\'s Tasks'; // Max's Tasks
}