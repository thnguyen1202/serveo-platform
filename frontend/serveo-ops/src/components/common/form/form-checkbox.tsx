import { Checkbox } from '@/components/ui/checkbox';
import { Field, FieldDescription, FieldError, FieldLabel } from '@/components/ui/field';
import { useFormField } from '@/hooks/use-form-field';
import type { ComponentProps } from 'react';
import type { FieldValues } from 'react-hook-form';
import type { BooleanFieldPath } from './form-types';

type FormCheckboxProps<T extends FieldValues> = {
  name: BooleanFieldPath<T>;
  label?: string;
  description?: string;
} & Omit<ComponentProps<typeof Checkbox>, 'name' | 'isSelected' | 'onChange'>;

export function FormCheckbox<T extends FieldValues>({ name, label, description, ...props }: FormCheckboxProps<T>) {
  const { field, error } = useFormField<T, BooleanFieldPath<T>>(name);

  const inputId = `field-${String(name).replace(/\./g, '-')}`;

  return (
    <Field orientation="horizontal" data-invalid={!!error}>
      <Checkbox
        {...props}
        id={inputId}
        name={field.name}
        isSelected={field.value}
        onChange={field.onChange}
        onBlur={field.onBlur}
        // ref={field.ref}
      />

      <div className="space-y-1">
        {label && (
          <FieldLabel htmlFor={inputId} className="font-normal">
            {label}
          </FieldLabel>
        )}

        {description && <FieldDescription>{description}</FieldDescription>}

        {error?.message && <FieldError>{error.message}</FieldError>}
      </div>
    </Field>
  );
}
