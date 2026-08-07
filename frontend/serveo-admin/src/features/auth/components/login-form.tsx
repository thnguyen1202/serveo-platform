import { useForm } from 'react-hook-form';
import { z } from 'zod';
import { useLogin } from '../hooks/use-login';
import { useNavigate } from '@tanstack/react-router';
import { handleFormApiError } from '@/hooks/use-form-error';
import { cn } from '@/lib/utils';
import { Alert, AlertDescription, AlertTitle } from '@/components/ui/alert';
import { AlertCircleIcon, LoaderCircle } from 'lucide-react';
import { FieldGroup } from '@/components/ui/field';
import { FormInput } from '@/components/common/form-input';
import { Button } from '@/components/ui/button';
import { zodResolver } from '@hookform/resolvers/zod';
import { Form } from '@/components/common/form';
import { deviceStorage } from '@/lib/device-storage';
import { toast } from 'sonner';

const formSchema = z.object({
  email: z.email(),
  password: z.string().min(1, 'validation.required'),
  deviceId: z.string().optional(),
  clientType: z.number().optional(),
});
type LoginFormValues = z.infer<typeof formSchema>;
type LoginFormProps = React.ComponentProps<'form'> & {
  redirectTo?: string;
};

export function LoginForm({ className, redirectTo, ...props }: LoginFormProps) {
  const form = useForm<LoginFormValues>({
    resolver: zodResolver(formSchema),
    defaultValues: {
      email: '',
      password: '',
      deviceId: deviceStorage.getDeviceId(),
      clientType: deviceStorage.getClientType(),
    },
  });

  const loginMutation = useLogin();
  const navigate = useNavigate();

  const rootError = form.formState.errors.root;

  async function onSubmit(values: LoginFormValues) {
    try {
      console.log('LoginForm:onSubmit', values);
      await loginMutation.mutateAsync(values);

      navigate({
        to: redirectTo ?? '/',
        replace: true,
      });
    } catch (error) {
      handleFormApiError(error, form);

      if (form.formState.errors) {
        let description = '';
        for (const [key, value] of Object.entries(form.formState.errors)) {
          if (key === 'deviceId') {
            description += value.message;
          } else if (key === 'clientType') {
            description += value.message;
          }
        }

        if (description) {
          toast.error('Login failed', { description });
        }
      }
    }
  }

  return (
    <Form form={form} onSubmit={form.handleSubmit(onSubmit)} className={cn(className)} {...props}>
      {rootError && (
        <Alert variant="destructive" className="max-w-md border-none p-0">
          <AlertCircleIcon />
          <AlertTitle>Login failed</AlertTitle>
          <AlertDescription className="text-[0.8rem]">{rootError.message}</AlertDescription>
        </Alert>
      )}

      <FieldGroup>
        <FormInput name="email" label="Email" placeholder="email@example.com" />

        <FormInput type="password" name="password" label="Password" placeholder="••••••••" />
      </FieldGroup>

      <Button type="submit" className="mt-2" isDisabled={loginMutation.isPending}>
        {loginMutation.isPending && <LoaderCircle className="animate-spin" />}
        Login
      </Button>
    </Form>
  );
}
