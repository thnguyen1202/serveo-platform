import { useEffect } from 'react';
import './App.css';
import { QueryClientProvider } from '@tanstack/react-query';
import { queryClient } from '@/lib/query/query.client';
import { RouterProvider as AriaRouterProvider } from 'react-aria-components';
import { createRouter, RouterProvider as TanStackRouterProvider } from '@tanstack/react-router';
import { routeTree } from './routeTree.gen';
import { ThemeProvider } from '@/app/providers/theme-provider';
import { FontProvider } from '@/app/providers/font-provider';
import { DirectionProvider } from '@/app/providers/direction-provider';
import { LoadingScreen } from '@/shared/components/loading-screen';
import { useAuthStore } from '@/app/bootstrap/auth.store';

// Create a new router instance
const router = createRouter({
  routeTree,
  context: { queryClient },
  defaultPreload: 'intent',
  defaultPreloadStaleTime: 0,
});

// Register the router instance for type safety
declare module '@tanstack/react-router' {
  interface Register {
    router: typeof router;
  }
}

function App() {
  return (
    <QueryClientProvider client={queryClient}>
      <ThemeProvider>
        <FontProvider>
          <DirectionProvider>
            <AuthInitializer />
          </DirectionProvider>
        </FontProvider>
      </ThemeProvider>
    </QueryClientProvider>
  );
}

function AppRouter() {
  return (
    <AriaRouterProvider navigate={(href) => router.navigate({ to: href })}>
      <TanStackRouterProvider router={router} />
    </AriaRouterProvider>
  );
}

function AuthInitializer() {
  const initialized = useAuthStore((state) => state.initialized);
  const initializing = useAuthStore((state) => state.initializing);
  const initialize = useAuthStore((state) => state.initialize);

  useEffect(() => {
    if (initialized || initializing) {
      return;
    }

    initialize();
  }, [initialized, initializing, initialize]);

  if (!initialized) {
    return <LoadingScreen />;
  }

  return <AppRouter />;
}

export default App;
