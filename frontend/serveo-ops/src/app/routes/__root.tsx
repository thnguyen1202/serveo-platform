/* eslint-disable react-refresh/only-export-components */
/* Tắt cảnh báo ESLint tại file __root.tsx, 
   Do biến Route không phải là một React Component, mà là một đối tượng cấu hình route
   ESLint hiểu nhầm rằng đây là một hằng số/biến thường chứ không phải component*/
import { type QueryClient } from '@tanstack/react-query';
import { createRootRouteWithContext, Outlet } from '@tanstack/react-router';
import { ReactQueryDevtools } from '@tanstack/react-query-devtools';
import { TanStackRouterDevtools } from '@tanstack/react-router-devtools';
import { Toaster } from '@/shared/components/ui/sonner';
import { NavigationProgress } from '@/shared/components/navigation-progress';
import { ErrorPage } from '@/shared/components/common/error-page';

interface RouterContext {
  queryClient: QueryClient;
}

export const Route = createRootRouteWithContext<RouterContext>()({
  component: RootComponent,
  notFoundComponent: NotFoundLayout,
  errorComponent: ErrorLayout,
});

export function RootComponent() {
  return (
    <>
      <NavigationProgress />
      <Outlet />
      <Toaster richColors />
      {import.meta.env.DEV && (
        <>
          <ReactQueryDevtools buttonPosition="bottom-left" />
          <TanStackRouterDevtools position="bottom-right" />
        </>
      )}
    </>
  );
}

export function NotFoundLayout() {
  return (
    <ErrorPage
      code={404}
      title="Oops! Page Not Found!"
      description={
        <>
          It seems like the page you're looking for
          <br />
          does not exist or might have been removed.
        </>
      }
    />
  );
}

type ErrorComponentLayoutProps = React.ComponentProps<'div'> & {
  minimal?: boolean;
};

export function ErrorLayout({ minimal = false, className }: ErrorComponentLayoutProps) {
  return (
    <ErrorPage
      code={500}
      minimal={minimal}
      className={className}
      title="Oops! Something went wrong"
      description={
        <>
          We apologize for the inconvenience.
          <br />
          Please try again later.
        </>
      }
    />
  );
}
