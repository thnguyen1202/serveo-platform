import { useController, useFormContext, type FieldPath, type FieldValues } from 'react-hook-form';

export function useFormField<T extends FieldValues>(name: FieldPath<T>) {
  const { control } = useFormContext<T>();

  return useController({
    name,
    control,
  });
}
