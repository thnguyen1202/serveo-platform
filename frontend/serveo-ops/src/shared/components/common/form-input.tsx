import { type FieldPathByValue, type FieldValues } from 'react-hook-form';
import { useTranslation } from 'react-i18next';

import { Input } from '@/shared/components/ui/input';
import { Field, FieldDescription, FieldError, FieldLabel } from '@/shared/components/ui/field';
import { useFormField } from '@/shared/hooks/use-form-field';
import { Password } from './password';

type FormInputAttribute = 'password' | 'text' | (string & {});

type FormInputProps<T extends FieldValues> = {
  name: FieldPathByValue<T, string>;
  label?: string | undefined;
  placeholder?: string | undefined;
  description?: string | undefined;
  required?: boolean | undefined;
  type?: FormInputAttribute | undefined;
};

export function FormInput<T extends FieldValues>({
  name,
  label,
  placeholder,
  description,
  required = false,
  type = 'text',
}: FormInputProps<T>) {
  const { t } = useTranslation('common');
  const { field, error } = useFormField<T, FieldPathByValue<T, string>>(name);

  const inputId = String(name);
  const Component = type === 'password' ? Password : Input;

  return (
    <Field className="gap-1">
      {label && (
        <FieldLabel htmlFor={inputId}>
          {label}
          {required && <span className="text-destructive"> *</span>}
        </FieldLabel>
      )}

      <Component
        {...field}
        id={inputId}
        value={(field.value as string | number | undefined) ?? ''}
        placeholder={placeholder ?? label}
        required={required}
        aria-invalid={!!error || undefined}
        data-invalid={!!error || undefined}
      />

      {description && <FieldDescription>{description}</FieldDescription>}

      {error && (
        <FieldError className="text-[0.8rem]">
          {t(error.message ?? '', { field: t(`fields.${field.name}`) })}
        </FieldError>
      )}
    </Field>
  );
}
