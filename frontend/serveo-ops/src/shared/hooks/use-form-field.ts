import { useController, useFormContext, type FieldPath, type FieldValues } from 'react-hook-form';

export function useFormField<T extends FieldValues, TName extends FieldPath<T>>(name: TName) {
  const { control } = useFormContext<T>();

  const { field, fieldState } = useController({
    name,
    control,
  });

  return {
    field,
    error: fieldState.error,
    invalid: fieldState.invalid,
  };
}

// export function useFormField<T extends FieldValues, TName extends FieldPath<T>>(name: TName) {
//   const { field, fieldState } = useController({
//     name,
//   } as UseControllerProps<T, TName>);

//   return {
//     field,
//     error: fieldState.error,
//     invalid: !!fieldState.error,
//   };
// }

// export function useFormField<T extends FieldValues>(name: FieldPath<T>) {
//   const { control } = useFormContext<T>();

//   return useController({
//     name,
//     control,
//   });
// }
