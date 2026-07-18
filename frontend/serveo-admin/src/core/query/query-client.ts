import { MutationCache, QueryClient } from '@tanstack/react-query';
import { toast } from 'sonner';

import { ApiException } from '../errors/api-exception';

export const queryClient = new QueryClient({
  defaultOptions: {
    queries: {
      staleTime: 1000 * 60 * 5, // Dữ liệu được xem là mới: 5 phút
      gcTime: 1000 * 60 * 30, // Cache giữ trong memory: 30 phút
      retry: 1,
      refetchOnWindowFocus: false,
    },

    mutations: {
      retry: false,
    },
  },
  mutationCache: new MutationCache({
    onError(error) {
      if (import.meta.env.VITE_ENABLE_LOG === 'true') {
        console.dir(error);
      }

      if (!(error instanceof ApiException)) {
        toast.error('Unexpected error', {
          description: error.message,
        });
      } else {
        if (error.status >= 500) {
          let description = '';
          if (Array.isArray(error.problem.errors)) {
            description = error.problem.errors.map((x) => x.message).join(' \n');
          }

          toast.error(error.message, {
            description: description,
            descriptionClassName: 'whitespace-pre-line leading-relaxed', // Helps preserve '\n' formatting and adds line spacing
          });
        }
      }
    },
  }),
});
