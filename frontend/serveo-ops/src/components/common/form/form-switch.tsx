import { Field, FieldDescription, FieldError, FieldLabel } from '@/components/ui/field';
import { Switch } from '@/components/ui/switch';
import { useFormField } from '@/hooks/use-form-field';
import type { ComponentProps } from 'react';
import type { FieldValues } from 'react-hook-form';
import type { BooleanFieldPath } from './form-types';

type FormSwitchProps<T extends FieldValues> = {
  name: BooleanFieldPath<T>;
  label?: string;
  description?: string;
} & Omit<ComponentProps<typeof Switch>, 'name' | 'isSelected' | 'onChange'>;

export function FormSwitch<T extends FieldValues>({ name, label, description, ...props }: FormSwitchProps<T>) {
  const { field, error } = useFormField<T, BooleanFieldPath<T>>(name);

  const inputId = `field-${String(name).replace(/\./g, '-')}`;

  return (
    <Field orientation="horizontal" data-invalid={!!error} className="gap-1">
      <Switch
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
