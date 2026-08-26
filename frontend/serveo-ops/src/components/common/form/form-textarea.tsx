import { Field, FieldDescription, FieldError, FieldLabel } from '@/components/ui/field';
import { Textarea } from '@/components/ui/textarea';
import { useFormField } from '@/hooks/use-form-field';
import type { ComponentProps } from 'react';
import type { FieldValues } from 'react-hook-form';
import type { StringFieldPath } from './form-types';

type FormTextareaProps<T extends FieldValues> = {
  name: StringFieldPath<T>;
  label?: string;
  description?: string;
} & Omit<ComponentProps<typeof Textarea>, 'name' | 'value' | 'defaultValue' | 'onChange' | 'onBlur'>;

export function FormTextarea<T extends FieldValues>({ name, label, description, ...props }: FormTextareaProps<T>) {
  const { field, error } = useFormField<T, StringFieldPath<T>>(name);

  const inputId = `field-${String(name).replace(/\./g, '-')}`;

  return (
    <Field className="gap-1">
      {label && <FieldLabel htmlFor={inputId}>{label}</FieldLabel>}

      <Textarea
        {...props}
        id={inputId}
        name={field.name}
        value={field.value ?? ''}
        onChange={field.onChange}
        onBlur={field.onBlur}
        ref={field.ref}
        aria-invalid={!!error}
      />

      {description && <FieldDescription>{description}</FieldDescription>}

      {error?.message && <FieldError>{error.message}</FieldError>}
    </Field>
  );
}
