import { useMutation } from '@tanstack/react-query';
import { useAuthStore } from '@/app/bootstrap/auth.store';
import { login } from './auth.api';

export function useLogin() {
  return useMutation({
    mutationFn: login,

    onSuccess: (result) => {
      useAuthStore.getState().setTokens(result); // Lưu token vào Zustand Store
    },
  });
}
