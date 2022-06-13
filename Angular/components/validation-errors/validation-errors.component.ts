import { Component, Input } from '@angular/core';
import { AbstractControl } from '@angular/forms';
import {ValidationErrorsService} from "./validation-errors.service";

interface Error {
  error: string;
  args: { [params: string]: string };
}

@Component({
  selector: 'app-validation-errors',
  templateUrl: './validation-errors.component.html',
  styleUrls: ['./validation-errors.component.scss']
})
export class ValidationErrorsComponent {
  readonly PREFIX_EXCLUDE = Object.freeze(['required']);

  @Input() prefix;
  @Input() control: AbstractControl;

  constructor(private errorsService: ValidationErrorsService) {}

  get validationErrors(): Error[] {
    return (
      this.control.errors &&
      Object.keys(this.control.errors).map((k) => ({
        error: k,
        args: this.control.errors[k] ?? null
      }))
    );
  }

  getError(error: Error): string {
    const errorKey =
      (this.prefix && !this.PREFIX_EXCLUDE.includes(error.error) ? `${this.prefix}_` : '') +
      error.error;
    return this.errorsService.instant(errorKey, error.args);
  }
}
