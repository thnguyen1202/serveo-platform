import { useSearch } from '@tanstack/react-router';

import { Card, CardContent, CardDescription, CardHeader, CardTitle } from '@/components/ui/card';
import { AuthLayout } from '../components/auth-layout';
import { LoginForm } from '../components/login-form';
import { useTranslation, Trans } from 'react-i18next';

export default function LoginPage() {
  const { t } = useTranslation('common');
  const { redirect } = useSearch({ from: '/(auth)/login' });

  return (
    <AuthLayout>
      <Card className="shadow-sm w-full sm:max-w-md">
        <CardHeader className="@container/card-header[container-type:normal]">
          <CardTitle className="text-lg tracking-tight">{t("login.title")}</CardTitle>
          <CardDescription className="">
            <Trans
              i18nKey="login.description"
              components={{
                br: <br className="max-sm:hidden" />
              }}
            />
          </CardDescription>
        </CardHeader>
        <CardContent>
          <LoginForm redirectTo={redirect} />
        </CardContent>
      </Card>
    </AuthLayout>
  );
}
