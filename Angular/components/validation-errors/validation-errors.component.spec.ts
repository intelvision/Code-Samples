import { ValidationErrorsComponent } from './validation-errors.component';
import { createComponentFactory, Spectator } from '@ngneat/spectator';
import { CoreModule } from '../../core.module';
import { FormControl, Validators } from '@angular/forms';

describe('ValidationErrorsComponent', () => {
  let spec: Spectator<ValidationErrorsComponent>;
  const createComponent = createComponentFactory({
    component: ValidationErrorsComponent,
    imports: [CoreModule],
    declarations: [ValidationErrorsComponent],
    detectChanges: false
  });

  beforeEach(() => {
    spec = createComponent();
  });

  it('should create', () => {
    spec.component.control = new FormControl();
    spec.detectChanges();

    expect(spec.component).toBeTruthy();
  });

  it('should return 0 errors, if control is valid', () => {
    spec.component.control = new FormControl();
    spec.detectChanges();

    expect(spec.component.validationErrors).toBeNull();
    expect(spec.query('div')).toBeNull();
    expect(spec.query('small')).toBeNull();
  });

  it('should render error if field is invalid and dirty and touched', () => {
    spec.component.control = new FormControl(null, Validators.required);
    spec.component.control.markAsDirty();
    spec.component.control.markAllAsTouched();
    spec.detectChanges();

    expect(spec.component.validationErrors.length).toEqual(1);
    expect(spec.query('div')).not.toBeNull();
    expect(spec.query('small')).not.toBeNull();
    expect(spec.query('small').textContent).toBe('Field is required');
  });

  it('should not render, if control is invalid, but not touched', () => {
    spec.component.control = new FormControl(null, Validators.required);
    spec.component.control.markAsDirty();
    spec.detectChanges();

    expect(spec.component.validationErrors.length).toEqual(1);
    expect(spec.query('div')).toBeNull();
    expect(spec.query('small')).toBeNull();
  });

  it('should not render, if control is invalid, but not dirty', () => {
    spec.component.control = new FormControl(null, Validators.required);
    spec.component.control.markAsTouched();
    spec.detectChanges();

    expect(spec.component.validationErrors.length).toEqual(1);
    expect(spec.query('div')).toBeNull();
    expect(spec.query('small')).toBeNull();
  });
});
