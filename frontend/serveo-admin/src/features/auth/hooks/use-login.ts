import { useMutation } from '@tanstack/react-query';
import { login } from '../api/auth.api';
import { useAuthStore } from '@/core/auth/auth.store';

export function useLogin() {
  return useMutation({
    mutationFn: login,

    onSuccess: (result) => {
      useAuthStore.getState().login(result);
    },
  });
}
