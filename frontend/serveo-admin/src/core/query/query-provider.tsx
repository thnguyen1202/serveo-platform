import { QueryClientProvider } from '@tanstack/react-query';
import { ReactQueryDevtools } from '@tanstack/react-query-devtools';
import type { ReactNode } from 'react';

import { Toaster } from '@/components/ui/sonner';

import { queryClient } from './query-client';

export function QueryProvider({ children }: { children: ReactNode }) {
  return (
    <QueryClientProvider client={queryClient}>
      {children}
      <Toaster richColors={true} />
      <ReactQueryDevtools initialIsOpen={false} />
    </QueryClientProvider>
  );
}
