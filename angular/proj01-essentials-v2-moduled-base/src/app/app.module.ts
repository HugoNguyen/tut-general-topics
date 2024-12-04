import { NgModule } from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";

import { AppComponent } from "./app.component";
import { HeaderComponent } from "./header/header.component";
import { UserComponent } from "./user/user.component";
import { TasksComponent } from "./tasks/tasks.component";


@NgModule({
    declarations: [AppComponent],
    bootstrap: [AppComponent],
    // The imports array is not just used for enabling standalone components
    // but also for including other modules
    imports: [BrowserModule, HeaderComponent, UserComponent, TasksComponent],

})
export class AppModule {

}