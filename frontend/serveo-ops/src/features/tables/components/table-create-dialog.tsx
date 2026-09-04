import { AlertError } from '@/shared/components/common/alert-error';
import { Form } from '@/shared/components/common/form';
import { FormInput } from '@/shared/components/common/form/form-input';
import { FieldGroup } from '@/shared/components/ui/field';
import { handleFormApiError } from '@/shared/hooks/use-form-error';
import { useForm } from 'react-hook-form';
import { toast } from 'sonner';
import { useDialog } from '../../../shared/context/dialog-context';
import { useEffect } from 'react';
import { DialogPost } from '@/shared/components/dialog-post';
import { useCreateTable } from '../tables.mutations';
import { tableCreateResolver, type TableCreateRequest } from '../tables.schema';

const DIALOG_TYPE = 'table-create';
type FieldValues = TableCreateRequest;


export function TableCreateDialog({ ...props }: React.ComponentProps<'form'>) {
  const { dialog, closeDialog } = useDialog();
  const createMutation = useCreateTable();

  if (dialog?.type !== DIALOG_TYPE) return;
  const isOpen = dialog?.type === DIALOG_TYPE;

  const form = useForm<FieldValues>({
    resolver: tableCreateResolver,
    defaultValues: {
      name: '',
      capacity: 2,
    },
  });

  // Reset form state whenever dialog opens/closes
  useEffect(() => {
    if (!isOpen) {
      form.reset();
    }
  }, [isOpen, form]);

  if (!isOpen) return null;

  const rootError = form.formState.errors.root;
  const formId = DIALOG_TYPE + '-form';

  async function onSubmit(data: FieldValues) {
    try {
      await createMutation.mutateAsync(data );
      toast.success('Tạo mới thành công!');
      closeDialog(); // close dialog
    } catch (error) {
      handleFormApiError(error, form);
    }
  }

  return (
    <DialogPost
      isOpen={isOpen}
      onOpenChange={(open) => {
        if (!open) {
          closeDialog();
        }
      }}
      dialogTitle={"Add New Table"}
      dialogDescription={
        <>
          {"Create new table here. "}
          Click save when you&apos;re done.
        </>
      }
      formId={formId}
      isPending={createMutation.isPending}
    >

      <Form id={formId} form={form} onSubmit={form.handleSubmit(onSubmit)} {...props}>
        {rootError && <AlertError title="Create table failed" message={rootError.message} />}

        <FieldGroup>
          <FormInput name="name" label="Name" placeholder="Name" />
          <FormInput name="capacity" label="Capacity" placeholder="Capacity" type="number" />
        </FieldGroup>
      </Form>
    </DialogPost>
  );

  // return (
  //   <Dialog
  //     className="sm:max-w-sm"
  //     isOpen={isOpen}
  //     onOpenChange={(open) => {
  //       if (!open) {
  //         closeDialog();
  //       }
  //     }}
  //   >
  //     <DialogHeader>
  //       <DialogTitle>{isUpdate ? 'Edit Category' : 'Add New Category'}</DialogTitle>
  //       <DialogDescription>
  //         {isUpdate ? 'Update the category here. ' : 'Create new category here. '}
  //         Click save when you&apos;re done.
  //       </DialogDescription>
  //     </DialogHeader>

  //     <Form id={formId} form={form} onSubmit={form.handleSubmit(onSubmit)} {...props}>
  //       {rootError && <AlertError title="Create menu failed" message={rootError.message} />}

  //       <FieldGroup>
  //         <FormInput name="name" label="Name" placeholder="Name" />
  //       </FieldGroup>
  //     </Form>

  //     <DialogFooter>
  //       <DialogClose variant="outline">Cancel</DialogClose>
  //       <ButtonSubmit form={formId} isPending={createMutation.isPending} text='Save changes' />
  //     </DialogFooter>
  //   </Dialog>
  // );
}
