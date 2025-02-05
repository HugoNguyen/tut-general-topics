import { NgModule } from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";
import { InvestmentModule } from "./investment.module";
import { AppComponent } from "./app.component";
import { HeaderComponent } from "./header/header.component";

@NgModule({
    imports: [BrowserModule, InvestmentModule],
    declarations: [AppComponent, HeaderComponent],
    bootstrap: [AppComponent]
})
export class AppModule {}