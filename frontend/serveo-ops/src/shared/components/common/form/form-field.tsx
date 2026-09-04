import { Field, FieldDescription, FieldError, FieldLabel } from '@/shared/components/ui/field';
import type { ReactNode } from 'react';

type FormFieldProps = {
  label?: ReactNode;
  description?: ReactNode;
  error?: string;
  children: ReactNode;
};

export function FormField({ label, description, error, children }: FormFieldProps) {
  return (
    <Field className="gap-1">
      {label && <FieldLabel>{label}</FieldLabel>}

      {children}

      {description && <FieldDescription>{description}</FieldDescription>}

      {error && <FieldError>{error}</FieldError>}
    </Field>
  );
}
