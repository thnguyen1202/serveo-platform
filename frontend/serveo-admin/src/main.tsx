import '@/i18n';
import './index.css';
import './app.css';
import { StrictMode, useEffect } from 'react';
import { createRoot } from 'react-dom/client';

import { QueryClientProvider } from '@tanstack/react-query';

import { createRouter, RouterProvider as TanStackRouterProvider } from '@tanstack/react-router';
import { routeTree } from './routeTree.gen.ts';

import { DirectionProvider } from './providers/direction-provider';
import { ThemeProvider } from './providers/theme-provider';
import { FontProvider } from './providers/font-provider';
import { queryClient } from './core/query/query.client.ts';
import { useAuthStore } from './core/auth/auth.store';
import { LoadingScreen } from './components/loading-screen';
import { RouterProvider as AriaRouterProvider } from 'react-aria-components';

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

createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <QueryClientProvider client={queryClient}>
      <ThemeProvider>
        <FontProvider>
          <DirectionProvider>
            <AuthInitializer />
          </DirectionProvider>
        </FontProvider>
      </ThemeProvider>
    </QueryClientProvider>
  </StrictMode>,
);

function AppRouter() {
  return (
    <AriaRouterProvider navigate={(href) => router.navigate({ to: href })}>
      <TanStackRouterProvider router={router} />
    </AriaRouterProvider>
  );
}

function AuthInitializer() {
  const initialize = useAuthStore((s) => s.initialize);
  const initialized = useAuthStore((s) => s.initialized);

  useEffect(() => {
    initialize();
  }, [initialize]);

  console.log('AuthInitializer render', initialized);

  if (!initialized) {
    return <LoadingScreen />;
  }

  return <AppRouter />;
}
