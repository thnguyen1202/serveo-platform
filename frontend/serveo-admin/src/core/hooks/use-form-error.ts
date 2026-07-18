import type { FieldValues, Path, UseFormReturn } from 'react-hook-form';

import { ApiException } from '../errors/api-exception';

export function applyServerErrors<T extends FieldValues>(form: UseFormReturn<T>, errors: Record<string, string[]>) {
  Object.entries(errors).forEach(([field, messages]) => {
    form.setError(field as Path<T>, {
      type: 'server',
      message: messages[0],
    });
  });
}

export function handleFormApiError<T extends FieldValues>(error: unknown, form: UseFormReturn<T>) {
  if (error instanceof ApiException && error.problem.errors) {
    if (error.problem.status === 400) {
      applyServerErrors(form, error.problem.errors);
    } else {
      form.setError('root', {
        type: 'server',
        message: error.problem.errors[0].message,
      });
    }
  }

  // toast.error('Unexpected error');
}
// export function useFormError() {
//   const applyErrors = (apiError: any, setError: any) => {
//     const errors = apiError.response?.data?.errors;

//     if (!errors) return;

//     Object.keys(errors).forEach((field) => {
//       setError(field, {
//         type: 'server',
//         message: errors[field][0],
//       });
//     });
//   };

//   return {
//     applyErrors,
//   };
// }
