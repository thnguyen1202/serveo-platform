import { zodResolver } from '@hookform/resolvers/zod';
import { AlertCircleIcon } from 'lucide-react';
import { FormProvider, useForm } from 'react-hook-form';

import { TextField } from '@/components/common/text-field';
import { Alert, AlertDescription, AlertTitle } from '@/components/ui/alert';
import { Button } from '@/components/ui/button';
import { handleFormApiError } from '@/core/hooks/use-form-error';

import { useLogin } from '../hooks/use-login';
import { type LoginFormValues, loginSchema } from '../schemas/login.schema';

export default function LoginPage() {
  const useMutation = useLogin();
  const form = useForm<LoginFormValues>({
    resolver: zodResolver(loginSchema),

    // defaultValues: {
    //   email: 'sampel@gmail.com',
    //   password: 'Qwe@1123',
    // },
  });

  // const {
  //   register,
  //   handleSubmit,
  //   formState: { errors },
  // } = useForm<LoginFormValues>({
  //   resolver: zodResolver(loginSchema),
  //   defaultValues: {
  //     email: 'sampel@gmail.com',
  //     password: 'Qwe@1123',
  //   },
  // });

  // login.mapper.ts // Tạo Mapper Function (phù hợp dự án lớn)
  // export function toLoginRequest(values: LoginFormValues): LoginRequest {
  //   return {
  //     username: values.username.trim(),
  //     password: values.password,
  //   };
  // }

  async function onSubmit(values: LoginFormValues) {
    try {
      await useMutation.mutateAsync(values);
    } catch (error) {
      handleFormApiError(error, form);
    }
  }

  return (
    <div className="h-screen flex items-center justify-center">
      <FormProvider {...form}>
        <form onSubmit={form.handleSubmit(onSubmit)} className="space-y-4">
          {form.formState.errors.root && (
            <Alert variant="destructive" className="max-w-md border-amber-200">
              <AlertCircleIcon />
              <AlertTitle>Login failed</AlertTitle>
              <AlertDescription>{form.formState.errors.root.message}</AlertDescription>
            </Alert>
          )}
          <TextField name="email" label="Email" />
          <TextField name="password" label="Password" />

          <Button type="submit" disabled={useMutation.isPending}>
            {useMutation.isPending ? 'Signing in...' : 'Sign In'}
          </Button>
        </form>
      </FormProvider>
    </div>
  );

  // return (
  //   <div className="h-screen flex items-center justify-center">
  //     <form onSubmit={handleSubmit(onSubmit)} className="space-y-4">
  //       <TextField name="email" label="Email" placeholder="Email" error={errors.email} register={register} />

  //       <TextField name="password" label="Password" placeholder="Pasword" error={errors.password} register={register} />

  //       {/* <pre>{JSON.stringify(errors, null, 2)}</pre> */}
  //       <Button type="submit" disabled={loginMutation.isPending}>
  //         {loginMutation.isPending ? 'Signing in...' : 'Sign In'}
  //       </Button>
  //     </form>
  //   </div>
  // );

  // return (
  //   <div className="h-screen flex items-center justify-center">
  //     <form onSubmit={form.handleSubmit(onSubmit)} className="space-y-4">
  //       <TextField name="email" label="Email" register={register} error={errors.email} placeholder="Email" />
  //       {/* <div>
  //         <Input placeholder="Email" {...form.register('email')} />

  //         {form.formState.errors.email && <p className="text-sm text-red-500">{form.formState.errors.email.message}</p>}
  //       </div> */}

  //       <div>
  //         <Input type="password" placeholder="Password" {...form.register('password')} />

  //         {form.formState.errors.password && (
  //           <p className="text-sm text-red-500">{form.formState.errors.password.message}</p>
  //         )}
  //       </div>
  //       {/* {errors.root && <Alert>{t(errors.root.message)}</Alert>} */}
  //       <Button type="submit" disabled={loginMutation.isPending}>
  //         {loginMutation.isPending ? 'Signing in...' : 'Sign In'}
  //       </Button>
  //     </form>
  //   </div>
  // );
}
