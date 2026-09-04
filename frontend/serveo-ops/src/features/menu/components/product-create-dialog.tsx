import { AlertError } from '@/shared/components/common/alert-error';
import { Form } from '@/shared/components/common/form';
import { FormInput } from '@/shared/components/common/form/form-input';
import { FieldGroup } from '@/shared/components/ui/field';
import { handleFormApiError } from '@/shared/hooks/use-form-error';
import { useForm } from 'react-hook-form';
import { toast } from 'sonner';
import { FormTextarea } from '@/shared/components/common/form/form-textarea';
import { useDialog } from '../../../shared/context/dialog-context';
import { useCreateProduct } from '../menu.mutations';
import { productCreateResolver, type ProductCreateFieldValues } from '../menu.schema';
import { DialogPost } from '@/shared/components/dialog-post';
import { useEffect } from 'react';

const DIALOG_TYPE = "product-create";

type FieldValues = ProductCreateFieldValues;

export function ProductCreateDialog({ ...props }: React.ComponentProps<'form'>) {
  const { dialog, closeDialog } = useDialog();
  const createMutation = useCreateProduct();

  if (dialog?.type !== DIALOG_TYPE) return;
  const isOpen = dialog?.type === DIALOG_TYPE;
  const menuId = dialog?.payload?.menuId ?? '';
  const categoryId = dialog?.payload?.categoryId ?? '';


  const form = useForm<FieldValues>({
    resolver: productCreateResolver,
    defaultValues: {
      categoryId: categoryId,
      name: '',
      price: 0,
    }
  });

  useEffect(() => {
    if (!isOpen) {
      form.reset();
    }
  }, [isOpen, form]);

  if (!isOpen) return null;

  const rootError = form.formState.errors.root;
  const formId = DIALOG_TYPE + '-form';


  async function onSubmit(data: FieldValues) {
    if (!menuId) { toast.error("Please select menu"); return; }
    if (!categoryId) { toast.error("Please select category"); return; }
console.log('onSubmit',{ menuId, data } );
    try {
      await createMutation.mutateAsync({ menuId, data });
      toast.success('Tạo mới thành công!');
      closeDialog();
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
      dialogTitle={"Add New Product"}
      dialogDescription={
        <>
          {"Create new product here. "}
          Click save when you&apos;re done.
        </>
      }
      formId={formId}
      isPending={createMutation.isPending}
    >

      <Form id={formId} form={form} onSubmit={form.handleSubmit(onSubmit)} {...props}>
        {rootError && <AlertError title="Create product failed" message={rootError.message} />}

        <FieldGroup>
          <FormInput name="name" label="Name" placeholder="Name" />
          <FormInput type='number' name="price" label="Price" placeholder="Price"/>
          <FormTextarea name="description" label="Description" placeholder="Description" />
        </FieldGroup>
      </Form>

    </DialogPost>
  );
}
