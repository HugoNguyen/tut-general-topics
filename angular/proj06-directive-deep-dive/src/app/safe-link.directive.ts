import { Directive } from "@angular/core";

@Directive({
    selector: 'a[appSafeLink]',
    standalone: true,
    host: {
        '(click)': 'onConfirmLeavePage($event)'
    }
})
export class SafeLinkDirective {
    constructor() {
        console.log('SafeLinkDirective is active');
    }

    onConfirmLeavePage(event: MouseEvent) {
        const wantsToLeave = window.confirm('Are you sure you want to leave?');

        if (wantsToLeave) {
            return true;
        }

        event.preventDefault();
        return false;
    }
}