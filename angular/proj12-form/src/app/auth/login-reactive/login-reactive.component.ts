import { Component, DestroyRef, inject, OnInit } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { debounceTime, of } from 'rxjs';

function mustContainQuestionMark(control: AbstractControl) {
  if (control.value.includes('?')) {
    return null;
  }

  return { doesNotContainQuestionMark: true };
}

function emailIsUnique(control: AbstractControl) {
  if (control.value !== 'test@example.com') {
    return of(null);
  }

  return of({ notunique: true });
}

@Component({
  selector: 'app-login-reactive',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './login-reactive.component.html',
  styleUrl: './login-reactive.component.css',
})
export class LoginReactiveComponent implements OnInit {
  private destroyRef = inject(DestroyRef);
  form = new FormGroup({
    email: new FormControl('', {
      validators: [ Validators.required, Validators.email ],
      asyncValidators: [ emailIsUnique ],
    }),
    password: new FormControl('', {
      validators: [ Validators.required, Validators.minLength(6), mustContainQuestionMark ],
    }),
  });

  get emailIsInvalid() {
    return (
      this.form.controls.email.touched
      && this.form.controls.email.dirty
      && this.form.controls.email.invalid
    );
  }

  get passwordIsInvalid() {
    return (
      this.form.controls.password.touched
      && this.form.controls.password.dirty
      && this.form.controls.password.invalid
    );
  }

  ngOnInit(): void {
    const savedForm = window.localStorage.getItem('saved-login-form');

    if (savedForm) {
      const loadedFormData = JSON.parse(savedForm);
      this.form.patchValue({ email: loadedFormData.email });
    }

    const subscription = this.form.valueChanges.pipe(debounceTime(500)).subscribe({
      next: (value) => {
        window.localStorage.setItem('saved-login-form', JSON.stringify({ email: value.email }));
      }
    });

    this.destroyRef.onDestroy(() => subscription.unsubscribe());
  }

  onSubmit() {
    if (this.form.invalid) {
      return;
    }
    const enteredEmail = this.form.value.email;
    const enteredPassword = this.form.value.password;
    console.log(enteredEmail, enteredPassword);

    this.form.reset();
  }
}