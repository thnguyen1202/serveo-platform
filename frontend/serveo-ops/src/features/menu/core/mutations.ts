import { useMutation } from '@tanstack/react-query';
import { create } from './api';

export function useCreateEntityMutation() {
  return useMutation({
    mutationFn: create,

    onSuccess: (result) => {
      console.log('useCreateEntity:onSuccess', result);
    },
  });
}
