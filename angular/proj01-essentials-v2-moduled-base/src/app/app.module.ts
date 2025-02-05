import { NgModule } from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";

import { AppComponent } from "./app.component";
import { HeaderComponent } from "./header/header.component";
import { UserComponent } from "./user/user.component";
import { SharedModule } from "./shared/shared.module";
import { TasksModule } from "./tasks/tasks.module";


@NgModule({
    declarations: [
        AppComponent,
        HeaderComponent,
        UserComponent,
    ],
    bootstrap: [AppComponent],
    // The imports array is not just used for enabling standalone components
    // but also for including other modules
    imports: [BrowserModule, SharedModule, TasksModule],

})
export class AppModule {}