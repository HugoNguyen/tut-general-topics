import { Component, ComponentFactoryResolver, DestroyRef, inject, ViewChild, ViewContainerRef } from '@angular/core';
import { AlertComponent } from '../alert/alert.component';
import { Subscription } from 'rxjs';
import { PlaceholderDirective } from '../placeholder/placeholder.directive';

@Component({
  selector: 'app-using-view-container-ref',
  standalone: true,
  imports: [PlaceholderDirective],
  templateUrl: './using-view-container-ref.component.html',
  styleUrl: './using-view-container-ref.component.css'
})
export class UsingViewContainerRefComponent {

  private destroyRef = inject(DestroyRef);
  private closeSub!: Subscription;

  // @ViewChild(PlaceholderDirective)
  // alertHost!: PlaceholderDirective;
  @ViewChild('vcr', { read: ViewContainerRef })
  alertHostViewRef!: ViewContainerRef;

  constructor() {
    this.destroyRef.onDestroy(() => {
      if (this.closeSub) {
        this.closeSub.unsubscribe();
      }
    });
  }
  
  alert() {
    this.showErrorAlert('Test An error occurred!');
  }

  private showErrorAlert(message: string) {
    // const hostViewContainerRef = this.alertHost.viewContainerRef;
    // hostViewContainerRef.clear();
    const hostViewContainerRef = this.alertHostViewRef;
    const componentRef = hostViewContainerRef.createComponent(AlertComponent);

    componentRef.instance.message = message;

    this.closeSub = componentRef.instance.close.subscribe(() => {
      this.closeSub.unsubscribe();
      hostViewContainerRef.clear();
    });
  }
}
