import { useSearch } from '@tanstack/react-router';

import { Card, CardContent, CardDescription, CardHeader, CardTitle } from '@/components/ui/card';
import { AuthLayout } from '../auth/components/auth-layout';
import { LoginForm } from '../auth/components/login-form';

export default function LoginPage() {
  const { redirect } = useSearch({ from: '/(auth)/login' });

  return (
    <AuthLayout>
      <Card className="shadow-sm w-full sm:max-w-md">
        <CardHeader className="@container/card-header[container-type:normal]">
          <CardTitle className="text-lg tracking-tight">Login</CardTitle>
          <CardDescription className="">
            {' '}
            Enter your email and password below to log into <br className="max-sm:hidden" /> your account.
          </CardDescription>
        </CardHeader>
        <CardContent>
          <LoginForm redirectTo={redirect} />
        </CardContent>
      </Card>
    </AuthLayout>
  );
}
