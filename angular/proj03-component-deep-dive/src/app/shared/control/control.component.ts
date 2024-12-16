import { AfterContentInit, afterNextRender, afterRender, Component, ContentChild, contentChild, ElementRef, HostBinding, HostListener, inject, input, ViewEncapsulation } from '@angular/core';

@Component({
  selector: 'app-control',
  standalone: true,
  imports: [],
  templateUrl: './control.component.html',
  styleUrl: './control.component.css',
  encapsulation: ViewEncapsulation.None,
  host: {
    class: 'control',
    '(click)': 'onClick()'
  }
})
export class ControlComponent implements AfterContentInit {
  // @HostBinding('class') className = 'control';
  // @HostListener('click') onClick() {
  //   console.log('Clicked');
  // }

  private el = inject(ElementRef);
  // @ContentChild('input') control?: ElementRef<HTMLInputElement | HTMLTextAreaElement>;
  private control = contentChild<ElementRef<HTMLInputElement | HTMLTextAreaElement>>('input');

  label = input.required();

  constructor() {
    /* Introduce afterRender and afterNextRender hooks */
    /* These hooks are special, they listen change on entire application */
    afterRender(() => {
      console.log('After render');
    });

    afterNextRender(() => {
      console.log('After next render');
    });
  }

  ngAfterContentInit(): void {
    // throw new Error('Method not implemented.');
  }

  onClick() {
    console.log(this.el);
    console.log(this.control());
  }
}
