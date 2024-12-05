import { Component, computed, inject, input, Input } from '@angular/core';
import { InvestmentResult } from '../investment-result.model';
import { CurrencyPipe } from '@angular/common';
import { InvestmentService } from '../investment.service';

@Component({
  selector: 'app-investment-results',
  standalone: true,
  imports: [CurrencyPipe],
  templateUrl: './investment-results.component.html',
  styleUrl: './investment-results.component.css'
})
export class InvestmentResultsComponent {

  private investmentService = inject(InvestmentService);

  //results = computed(() => this.investmentService.resultsData());
  //or
  results = this.investmentService.resultsData.asReadonly();
}
