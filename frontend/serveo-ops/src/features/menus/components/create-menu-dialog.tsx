import { AlertError } from '@/shared/components/common/alert-error';
import { Form } from '@/shared/components/common/form';
import { FormInput } from '@/shared/components/common/form/form-input';
import {
  Dialog,
  DialogClose,
  DialogDescription,
  DialogFooter,
  DialogHeader,
  DialogTitle,
} from '@/shared/components/ui/dialog';
import { FieldGroup } from '@/shared/components/ui/field';
import { handleFormApiError } from '@/shared/hooks/use-form-error';
import { zodResolver } from '@hookform/resolvers/zod';
import { useForm } from 'react-hook-form';
import z from 'zod';
import { toast } from 'sonner';
import { Button } from '@/shared/components/ui/button';
import { LoaderCircle } from 'lucide-react';
import { FormTextarea } from '@/shared/components/common/form/form-textarea';
import { useMenusContext } from '../context/use-menus-context';
import { useCreateMenu } from '../menus.mutations';

const formSchema = z.object({
  name: z.string().min(2),
  description: z.string(),
});

type MenuFormValues = z.infer<typeof formSchema>;

type MenuFormProps = React.ComponentProps<'form'> & {
  currentRow?: any;
};

export function CreateMenuDialog({ ...props }: MenuFormProps) {
  const { open, setOpen } = useMenusContext();
  const isOpen = open === 'create';
  const isUpdate = false;
  const formId = open + '-menu-form';

  const form = useForm<MenuFormValues>({
    resolver: zodResolver(formSchema),
    defaultValues: {
      name: '',
      description: '',
    },
  });

  const rootError = form.formState.errors.root;

  const createMutation = useCreateMenu();

  async function onSubmit(values: MenuFormValues) {
    try {
      await createMutation.mutateAsync(values);
      setOpen(null); // close dialog
      toast.success('Tạo mới thành công!');
      form.reset();
      console.log('onSubmit', values);
    } catch (error) {
      handleFormApiError(error, form);
    }
  }

  return (
    <Dialog
      className="sm:max-w-sm"
      isOpen={isOpen}
      onOpenChange={(value: any) => {
        if (!value) {
          setOpen(null);
        }
      }}
    >
      <DialogHeader>
        <DialogTitle>{isUpdate ? 'Edit Menu' : 'Add New Menu'}</DialogTitle>
        <DialogDescription>
          {isUpdate ? 'Update the menu here. ' : 'Create new menu here. '}
          Click save when you&apos;re done.
        </DialogDescription>
      </DialogHeader>

      <Form id={formId} form={form} onSubmit={form.handleSubmit(onSubmit)} {...props}>
        {rootError && <AlertError title="Create menu failed" message={rootError.message} />}

        <FieldGroup>
          <FormInput name="name" label="Name" placeholder="Name" />
          <FormTextarea name="description" label="Description" placeholder="Description" />
        </FieldGroup>
      </Form>

      <DialogFooter>
        <DialogClose variant="outline">Cancel</DialogClose>
        <Button form={formId} type="submit" isDisabled={createMutation.isPending}>
          {createMutation.isPending && <LoaderCircle className="animate-spin" />}
          Save changes
        </Button>
      </DialogFooter>
    </Dialog>
  );
}
