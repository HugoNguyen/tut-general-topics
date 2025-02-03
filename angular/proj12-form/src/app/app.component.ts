import { Component } from '@angular/core';

import { LoginComponent } from './auth/login/login.component';
import { LoginReactiveComponent } from "./auth/login-reactive/login-reactive.component";

@Component({
  selector: 'app-root',
  standalone: true,
  templateUrl: './app.component.html',
  imports: [LoginComponent, LoginReactiveComponent],
})
export class AppComponent {}
