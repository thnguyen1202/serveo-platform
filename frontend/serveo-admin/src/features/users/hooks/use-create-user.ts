import { useMutation, useQueryClient } from '@tanstack/react-query';

import { queryKeys } from '@/core/query/query-keys';

import { createUser } from '../api/users.api';

export function useCreateUser() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: createUser,

    onSuccess() {
      queryClient.invalidateQueries({
        queryKey: queryKeys.users.all,
      });
      //   queryClient.prefetchQuery({
      //     queryKey: ['users', id],
      //     queryFn: () => getUser(id),
      //   });
    },
  });
}
