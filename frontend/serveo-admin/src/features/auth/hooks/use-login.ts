//import { useAuthStore } from '@/core/auth/auth.store';
import { useMutation } from '@tanstack/react-query';

import { tokenStorage } from '@/core/auth/token-storage';

import { login } from '../api/auth.api';
//import type { LoginRequest } from '../types/auth.types';

export function useLogin() {
  return useMutation({
    //mutationFn: (request: LoginRequest) => login(request),
    mutationFn: login,

    onSuccess: (result) => {
      tokenStorage.setAccessToken(result.accessToken);
      tokenStorage.setRefreshToken(result.refreshToken);
    },
  });
}

// export function useLogin() {
//   async function execute(username: string, password: string) {
//     const result = await login({
//       username,
//       password,
//     });

//     tokenStorage.setAccessToken(result.accessToken);

//     tokenStorage.setRefreshToken(result.refreshToken);
//   }

//   return {
//     execute,
//   };
// }
