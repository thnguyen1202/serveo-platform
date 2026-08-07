import { useMutation, useQuery } from '@tanstack/react-query';
import { create, getPaged } from './api';
import type { PagingFilter } from '@/lib/paging';

function useCreateEntity() {
  return useMutation({
    mutationFn: create,

    onSuccess: (result) => {
      console.log('useCreateEntity:onSuccess', result);
    },
  });
}

// function useUpdateEntity() {
//   return useMutation({
//     mutationFn: login,

//     onSuccess: (result) => {
//       console.log('useCreateEntity:onSuccess', result);
//     },
//   });
// }

// function useDeleteEntity() {
//   return useMutation({
//     mutationFn: login,

//     onSuccess: (result) => {
//       console.log('useCreateEntity:onSuccess', result);
//     },
//   });
// }

export function useActions() {
  return {
    createMutation: useCreateEntity(),
    // update: useUpdateEntity(),
    // delete: useDeleteEntity(),
  };
}

interface PagingParams extends PagingFilter {
  sort?: string;
}
export function usePagingQuery(params: PagingParams) {
  return useQuery({
    queryKey: ['tasks', params],
    queryFn: getPaged,
  });
}
