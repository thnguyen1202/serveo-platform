import { useMutation, useQueryClient } from '@tanstack/react-query';
import { createMenu } from './menus.api';

export function useCreateMenu() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: createMenu,

    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['menus'] });
    },
  });
}
