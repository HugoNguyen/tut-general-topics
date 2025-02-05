import { AsyncPipe, NgComponentOutlet } from '@angular/common';
import { Component, Type } from '@angular/core';

@Component({
  selector: 'app-lazy-loading',
  standalone: true,
  imports: [NgComponentOutlet, AsyncPipe],
  templateUrl: './lazy-loading.component.html',
  styleUrl: './lazy-loading.component.css'
})
export class LazyLoadingComponent {

  componentRef!: Type<any>;
  time = 0;

  constructor() {
    this.lazyRender();
  }

  async lazyRender() {
    const start = new Date();
    const { LargeComponent } = await import('../large/large.component');

    this.componentRef = LargeComponent;
    this.time = new Date().getTime() - start.getTime();
  }
}
