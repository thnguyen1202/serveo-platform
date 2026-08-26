import { create } from 'zustand';
import type { AuthUserResponse } from './auth.types';
import { getTokenExpiresAt } from '@/lib/jwt';
import { csrfToken, me, refreshToken } from './auth.api';

export interface AuthState {
  accessToken: string | null;
  csrfToken: string | null;

  user: AuthUserResponse | null;
  permissions: string[];

  isAuthenticated: boolean;
  isExpiringSoon: boolean;

  initialized: boolean;
  initializing: boolean;

  initialize: () => Promise<void>;
  setTokens: (data: { accessToken: string }) => void;
  setUser: (data: any) => void;
  clear: () => void;
}

export const useAuthStore = create<AuthState>((set, get) => ({
  accessToken: null,
  csrfToken: null,

  user: null,
  permissions: [],

  isAuthenticated: false,
  isExpiringSoon: false,

  initialized: false,
  initializing: false,

  setTokens(data) {
    const expiresAt = getTokenExpiresAt(data.accessToken);
    const isExpiringSoon = expiresAt != null ? expiresAt - Date.now() < 60_000 : false;

    set({
      accessToken: data.accessToken,
      isAuthenticated: true,
      isExpiringSoon: isExpiringSoon,
    });
  },

  setUser(data) {
    set({
      user: data,
    });
  },

  clear() {
    set({
      accessToken: null,
      csrfToken: null,
      user: null,
      permissions: [],
      isAuthenticated: false,
      isExpiringSoon: false,
    });
  },

  initialize: async () => {
    if (get().initialized || get().initializing) {
      return;
    }

    set({ initializing: true });

    try {
      // 1. Bootstrap CSRF
      const csrf = await csrfToken();
      set({ csrfToken: csrf.csrfToken });

      // 2. Try restore session
      const tokens = await refreshToken();
      get().setTokens(tokens);

      // 3. Load current user
      const user = await me();
      get().setUser(user);
    } catch (error) {
      if (import.meta.env.DEV) console.error(error);
      get().clear();
    } finally {
      set({
        initialized: true,
        initializing: false,
      });
    }
  },
}));
