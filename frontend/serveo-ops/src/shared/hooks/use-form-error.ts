import { ApiException } from '@/lib/axios/api.exception';
import type { FieldValues, Path, UseFormReturn } from 'react-hook-form';

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
}
