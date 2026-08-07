import { Button } from '@/components/ui/button';
import { Sheet, SheetDescription, SheetFooter, SheetHeader, SheetTitle } from '@/components/ui/sheet';
import { useTenants } from '../../core/hook';
import { Form } from '@/components/common/form';
import { cn } from '@/lib/utils';
import { LoaderCircle } from 'lucide-react';
import { FieldGroup } from '@/components/ui/field';
import { FormInput } from '@/components/common/form-input';
import z from 'zod';
import { useForm } from 'react-hook-form';
import { zodResolver } from '@hookform/resolvers/zod';
import { handleFormApiError } from '@/hooks/use-form-error';
import { AlertError } from '@/components/common/alert-error';
import { useActions } from '../../core/use-actions';
import { toast } from 'sonner';

export const formSchema = z.object({
  name: z.string().min(1),
});

export type TenantFormValues = z.infer<typeof formSchema>;

type TenantFormProps = React.ComponentProps<'form'> & {};

export function CreateTenantDrawer({ className, ...props }: TenantFormProps) {
  const { open, setOpen } = useTenants();
  const isOpen = open === 'create';
  const isUpdate = false;
  const formId = open + '-tenant-form';

  const form = useForm<TenantFormValues>({
    resolver: zodResolver(formSchema),
    defaultValues: {
      name: '',
    },
  });

  const rootError = form.formState.errors.root;

  const { createMutation } = useActions();

  async function onSubmit(values: TenantFormValues) {
    try {
      //await tenantsMutation.mutateAsync(values);
      setOpen(null); // close dialog
      toast.success('Tạo mới thành công!');
      form.reset();
      console.log('onSubmit', values);
    } catch (error) {
      handleFormApiError(error, form);
    }
  }

  return (
    <Sheet
      isOpen={isOpen}
      onOpenChange={(value: any) => {
        if (!value) {
          setOpen(null);
        }
      }}
    >
      <SheetHeader className="text-start">
        <SheetTitle>{isUpdate ? 'Update' : 'Create'} Tenant</SheetTitle>
        <SheetDescription>
          {isUpdate
            ? 'Update the tenant by providing necessary info.'
            : 'Add a new tenant by providing necessary info.'}
          Click save when you&apos;re done.
        </SheetDescription>
      </SheetHeader>

      <Form
        id={formId}
        form={form}
        onSubmit={form.handleSubmit(onSubmit)}
        className={cn(className, 'space-y-6 overflow-y-auto px-4')}
        {...props}
      >
        {rootError && <AlertError title="Tenant failed" message={rootError.message} />}

        <FieldGroup>
          <FormInput name="name" label="Name" placeholder="Name" />
        </FieldGroup>
      </Form>

      <SheetFooter className="gap-2">
        <Button form={formId} type="submit" isDisabled={createMutation.isPending}>
          {createMutation.isPending && <LoaderCircle className="animate-spin" />}
          Save changes
        </Button>
      </SheetFooter>
    </Sheet>
  );
}
