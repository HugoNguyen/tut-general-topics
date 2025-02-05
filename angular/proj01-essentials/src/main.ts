import { bootstrapApplication } from '@angular/platform-browser';

import { AppComponent } from './app/app.component';

//Renders a standalone component (AppComponent) as the application's root component
bootstrapApplication(AppComponent).catch((err) => console.error(err));
