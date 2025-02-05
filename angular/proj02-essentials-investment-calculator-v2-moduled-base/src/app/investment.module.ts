import { NgModule } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { CommonModule } from "@angular/common";
import { UserInputComponent } from "./user-input/user-input.component";
import { InvestmentResultsComponent } from "./investment-results/investment-results.component";


@NgModule({
    imports: [FormsModule, CommonModule],
    declarations: [UserInputComponent, InvestmentResultsComponent],
    exports: [UserInputComponent, InvestmentResultsComponent]
})
export class InvestmentModule {}