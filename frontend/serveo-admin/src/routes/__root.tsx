import { type QueryClient } from '@tanstack/react-query';
import { createRootRouteWithContext, Outlet } from '@tanstack/react-router';
import { ReactQueryDevtools } from '@tanstack/react-query-devtools';
import { TanStackRouterDevtools } from '@tanstack/react-router-devtools';
import { Toaster } from '@/components/ui/sonner';
import { NavigationProgress } from '@/components/navigation-progress';
import { ErrorPage } from '@/components/common/error-page';
import type { AuthUser } from '@/core/auth/auth.types';

interface AuthState {
  user: AuthUser | null;
  permissions: string[];
  isAuthenticated: boolean;
  isLoading: boolean;

  logout: () => void;
}

interface RouterContext {
  queryClient: QueryClient;
  // auth: AuthState;
}

export const Route = createRootRouteWithContext<RouterContext>()({
  component: RootComponent,
  notFoundComponent: NotFoundLayout,
  errorComponent: ErrorLayout,
});

function RootComponent() {
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

function NotFoundLayout() {
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

function ErrorLayout({ minimal = false, className }: ErrorComponentLayoutProps) {
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

// export function NotFoundComponentLayout() {
//    const navigate = useNavigate();
//   const { history } = useRouter();
//   return (
//     <div className="h-svh">
//       <div className="m-auto flex h-full w-full flex-col items-center justify-center gap-2">
//         <h1 className="text-[7rem] leading-tight font-bold">404</h1>
//         <span className="font-medium">Oops! Page Not Found!</span>
//         <p className="text-center text-muted-foreground">
//           It seems like the page you're looking for <br />
//           does not exist or might have been removed.
//         </p>
//         <div className="mt-6 flex gap-4">
//           <Button variant="outline" onClick={() => history.go(-1)}>
//             Go Back
//           </Button>
//           <Button onClick={() => navigate({ to: '/' })}>Back to Home</Button>
//         </div>
//       </div>
//     </div>
//   );
// }

// type GeneralErrorProps = React.HTMLAttributes<HTMLDivElement> & {
//   minimal?: boolean;
// };

// export function ErrorComponentLayout({ className, minimal = false }: GeneralErrorProps) {
//    const navigate = useNavigate();
//   const { history } = useRouter();
//   return (
//     <div className={cn('h-svh w-full', className)}>
//       <div className="m-auto flex h-full w-full flex-col items-center justify-center gap-2">
//         {!minimal && <h1 className="text-[7rem] leading-tight font-bold">500</h1>}
//         <span className="font-medium">Oops! Something went wrong {`:')`}</span>
//         <p className="text-center text-muted-foreground">
//           We apologize for the inconvenience. <br /> Please try again later.
//         </p>
//         {!minimal && (
//           <div className="mt-6 flex gap-4">
//             <Button variant="outline" onClick={() => history.go(-1)}>
//               Go Back
//             </Button>
//             <Button onClick={() => navigate({ to: '/' })}>Back to Home</Button>
//           </div>
//         )}
//       </div>
//     </div>
//   );
// }
