import { FormProvider, type FieldValues, type UseFormReturn } from 'react-hook-form';
import { cn } from '@/lib/utils';
import type { ReactNode } from 'react';

interface Props<T extends FieldValues> extends React.ComponentProps<'form'> {
  form: UseFormReturn<T>;
  children: ReactNode;
}

export function Form<T extends FieldValues>({ form, className, children, ...props }: Props<T>) {
  return (
    <FormProvider {...form}>
      <form className={cn('grid gap-3', className)} {...props}>
        {children}
      </form>
    </FormProvider>
  );
}
