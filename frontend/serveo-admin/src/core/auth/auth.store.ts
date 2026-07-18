import { create } from 'zustand';

import type { UserInfo } from '@/features/auth/types/auth.types';

interface AuthState {
  user?: UserInfo;
  isAuthenticated: boolean;
  isLoading: boolean;

  setUser: (user: UserInfo) => void;
  logout: () => void;
}

export const useAuthStore = create<AuthState>((set) => ({
  user: undefined,
  isAuthenticated: false,
  isLoading: false,

  setUser(user) {
    set({
      user,
      isAuthenticated: true,
    });
  },

  logout() {
    set({
      user: undefined,
      isAuthenticated: false,
    });
  },
}));
