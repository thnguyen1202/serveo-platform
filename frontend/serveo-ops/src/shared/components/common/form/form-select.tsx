import { Button } from '@/shared/components/ui/button';
import { Field, FieldDescription, FieldError, FieldLabel } from '@/shared/components/ui/field';
import { Popover } from '@/shared/components/ui/popover';
import { Select, SelectValue } from '@/shared/components/ui/select';
import { useFormField } from '@/shared/hooks/use-form-field';
import type { ReactNode } from 'react';
import { ListBox } from 'react-aria-components';
import type { FieldValues } from 'react-hook-form';
import type { StringFieldPath } from './form-types';

type FormSelectProps<T extends FieldValues> = {
  name: StringFieldPath<T>;
  label?: string;
  description?: string;
  placeholder?: string;
  children: ReactNode;
};

export function FormSelect<T extends FieldValues>({ name, label, description, children }: FormSelectProps<T>) {
  const { field, error } = useFormField<T, StringFieldPath<T>>(name);

  const inputId = `field-${String(name).replace(/\./g, '-')}`;

  return (
    <Field className="gap-1">
      {label && <FieldLabel>{label}</FieldLabel>}

      <Select
        selectedKey={field.value ?? null}
        onSelectionChange={(key) => {
          field.onChange(key);
        }}
        onBlur={field.onBlur}
      >
        <Button id={inputId} aria-invalid={!!error}>
          <SelectValue
          //   placeholder={placeholder}
          />
        </Button>

        <Popover>
          <ListBox>{children}</ListBox>
        </Popover>
      </Select>

      {description && <FieldDescription>{description}</FieldDescription>}

      {error?.message && <FieldError>{error.message}</FieldError>}
    </Field>
  );
}
