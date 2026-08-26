import { Field, FieldDescription, FieldLabel } from '@/components/ui/field';
import { Checkbox } from '@/components/ui/checkbox';
import type { FieldPathByValue, FieldValues } from 'react-hook-form';
import { useFormField } from '@/hooks/use-form-field';
import type { ComponentProps } from 'react';

type BooleanFieldPath<T extends FieldValues> = FieldPathByValue<T, boolean>;

type FormCheckboxProps<T extends FieldValues> = {
  name: BooleanFieldPath<T>;
  label?: string;
  description?: string;
} & Omit<ComponentProps<typeof Checkbox>, 'name' | 'isSelected' | 'onChange'>;

export function FormCheckbox<T extends FieldValues>({ name, label, description, ...props }: FormCheckboxProps<T>) {
  const { field } = useFormField(name);
  const inputId = `field-${String(name).replace(/\./g, '-')}`;

  return (
    <Field orientation="horizontal">
      <Checkbox
        {...props}
        id={inputId}
        name={field.name}
        isSelected={field.value}
        onChange={field.onChange}
        onBlur={field.onBlur}
      />

      <div className="space-y-1">
        {label && (
          <FieldLabel htmlFor={inputId} className="font-normal">
            {label}
          </FieldLabel>
        )}

        {description && <FieldDescription>{description}</FieldDescription>}
      </div>
    </Field>
  );
}
