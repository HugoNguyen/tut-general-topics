import { Component, computed, DestroyRef, inject, input, OnInit, signal } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { UsersService } from '../users.service';

@Component({
  selector: 'app-user-tasks',
  standalone: true,
  templateUrl: './user-tasks.component.html',
  styleUrl: './user-tasks.component.css',
})
export class UserTasksComponent implements OnInit {
  // Op1.1: Extracting Dynamic Route Parameters via @Input()
  // @Input({required: true}) userId!: string;

  // Op1.2: Extracting Dynamic Route Parameters via input()
  // userId = input.required<string>();
  
  // Op2: Extracting Dynamic Route Parameters via ActiviatedRoute
  userId = signal<string>('');

  private usersService = inject(UsersService);
  private activatedRoute = inject(ActivatedRoute);
  private destroyRef = inject(DestroyRef);

  userName = computed(() => this.usersService.users.find(u => u.id === this.userId())?.name);

  ngOnInit() {
    const subscription = this.activatedRoute.paramMap.subscribe({
      next: paramMap => {
        this.userId.set(paramMap.get('userId') || '');
      },
    });

    this.destroyRef.onDestroy(() => subscription.unsubscribe());
  }
}
