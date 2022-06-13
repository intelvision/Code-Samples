import { Injectable } from '@angular/core';
import { VALIDATION_ERRORS_DICTIONARY } from '../components/validation-errors/validation-errors';

@Injectable({
  providedIn: 'root'
})
export class ValidationErrorsService {
  private readonly _errorsDictionary = Object.freeze(VALIDATION_ERRORS_DICTIONARY);

  instant(param: string, args?: { [property: string]: string }): string {
    if (param in this._errorsDictionary) {
      let error = this._errorsDictionary[param];

      args &&
        Object.keys(args).forEach((key: string) => {
          while (error.includes(`{{${key}}}`)) {
            error = error.replace(`{{${key}}}`, args[key]);
          }
        });

      return error;
    }

    return param;
  }
}
