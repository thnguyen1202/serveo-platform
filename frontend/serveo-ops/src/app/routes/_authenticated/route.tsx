import { createFileRoute, redirect } from '@tanstack/react-router';
import { useAuthStore } from '@/app/bootstrap/auth.store';
import { AuthenticatedLayout } from '@/app/layouts/authenticated-layout';

export const Route = createFileRoute('/_authenticated')({
  beforeLoad: ({ location }) => {
    const { isAuthenticated } = useAuthStore.getState();

    if (!isAuthenticated) {
      throw redirect({
        to: '/login',
        search: {
          redirect: location.href,
        },
      });
    }
  },

  component: AuthenticatedLayout,
});
