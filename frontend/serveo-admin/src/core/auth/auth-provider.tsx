import { useEffect } from 'react';

import { getCurrentUser } from '@/features/auth/api/auth.api';

import { useAuthStore } from './auth.store';
import { tokenStorage } from './token-storage';

export function AuthProvider({ children }) {
  useEffect(() => {
    const token = tokenStorage.getAccessToken();

    if (token) {
      getCurrentUser().then((user) => {
        useAuthStore.getState().setUser(user);
      });
    }
  }, []);

  return children;
}
