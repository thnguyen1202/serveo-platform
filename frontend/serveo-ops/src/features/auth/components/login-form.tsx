import { useForm } from 'react-hook-form';
import { z } from 'zod';
import { useLogin } from '../auth.mutations';
import { useNavigate } from '@tanstack/react-router';
import { handleFormApiError } from '@/shared/hooks/use-form-error';
import { cn } from '@/lib/utils';
import { LoaderCircle } from 'lucide-react';
import { FieldGroup } from '@/shared/components/ui/field';
import { FormInput } from '@/shared/components/common/form-input';
import { Button } from '@/shared/components/ui/button';
import { zodResolver } from '@hookform/resolvers/zod';
import { Form } from '@/shared/components/common/form';
import { deviceStorage } from '@/shared/config/device.storage';
import { toast } from 'sonner';
import { FormCheckbox } from '@/shared/components/common/form-checkbox';
import { useTranslation } from 'react-i18next';
import { AlertError } from '@/shared/components/common/alert-error';

const loginSchema = z.object({
  email: z.email({ message: 'validation.invalidEmail' }).min(1, 'validation.required'),
  password: z.string().min(1, 'validation.required'),
  isRemember: z.boolean(),
  deviceId: z.string().optional(),
  clientType: z.number().optional(),
});
type LoginFormValues = z.infer<typeof loginSchema>;
type LoginFormProps = React.ComponentProps<'form'> & {
  redirectTo?: string;
};

export function LoginForm({ className, redirectTo, ...props }: LoginFormProps) {
  const { t } = useTranslation('common');
  const form = useForm<LoginFormValues>({
    resolver: zodResolver(loginSchema),
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
          toast.error(t('error.login_failed'), { description });
        }
      }
    }
  }

  return (
    <Form form={form} onSubmit={form.handleSubmit(onSubmit)} className={cn(className)} {...props}>
      {rootError && <AlertError title={t('error.login_failed')} message={rootError.message} />}

      <FieldGroup>
        <FormInput name="email" label={t('fields.email')} placeholder="email@example.com" />

        <FormInput type="password" name="password" label={t('fields.password')} placeholder="••••••••" />

        <FormCheckbox name="isRemember" label={t('login.remember')} />
      </FieldGroup>

      <Button type="submit" className="mt-2" isDisabled={loginMutation.isPending}>
        {loginMutation.isPending && <LoaderCircle className="animate-spin" />}
        {t('login.submit')}
      </Button>
    </Form>
  );
}
