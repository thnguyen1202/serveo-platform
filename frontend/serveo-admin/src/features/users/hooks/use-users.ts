/**Query Hook */
import { useQuery } from '@tanstack/react-query';

import { queryKeys } from '@/core/query/query-keys';

import { getUsers } from '../api/users.api';
import type { UserPageRequest } from '../types/user.types';

export function useUsers(filter: UserPageRequest) {
  return useQuery({
    // queryKey: queryKeys.users.all,
    // queryFn: getUsers,
    // queryKey: ['users', page, search, status, role],
    // queryFn: () => getUsers(page),

    queryKey: queryKeys.users.list(filter),
    queryFn: () => getUsers(filter),
  });
}
