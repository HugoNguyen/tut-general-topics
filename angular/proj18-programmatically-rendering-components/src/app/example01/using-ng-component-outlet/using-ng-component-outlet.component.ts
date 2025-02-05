import { Component, Type } from '@angular/core';
import { UserInfoComponent } from '../user-info/user-info.component';
import { AdminInfoComponent } from '../admin-info/admin-info.component';
import { NgComponentOutlet } from '@angular/common';

@Component({
  selector: 'app-using-ng-component-outlet',
  standalone: true,
  imports: [NgComponentOutlet],
  templateUrl: './using-ng-component-outlet.component.html',
  styleUrl: './using-ng-component-outlet.component.css'
})
export class UsingNgComponentOutletComponent {

  infoComponent!: Type<UserInfoComponent | AdminInfoComponent>;

  constructor() { 
    this.changeInfoComponent();
  }

  changeInfoComponent() {
    const random = Math.random();

    if (random > 0.5) {
      this.infoComponent = UserInfoComponent;
      return;
    }

    this.infoComponent = AdminInfoComponent;
  }
}
