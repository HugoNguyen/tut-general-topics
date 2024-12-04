import { Component } from "@angular/core";

@Component({
    selector: 'app-header',
    standalone: true, // true is default in >= angular v19
    templateUrl: './header.component.html',
    styleUrl: './header.component.css'
})
export class HeaderComponent {}