import { create } from 'zustand';
import type { AuthUser } from './auth.types';
import { tokenStorage } from './token-storage';
import { me } from './auth.api';

export interface AuthState {
  user: AuthUser | null;
  permissions: string[];

  isAuthenticated: boolean;
  isLoading: boolean;
  initialized: boolean;
  initializing: boolean;

  setUser: (user: AuthUser) => void;
  login(data: any): void;
  logout: () => void;
  initialize: () => Promise<void>;
}

console.log('AUTH STORE CREATED');

export const useAuthStore = create<AuthState>((set, get) => ({
  user: null,
  permissions: [],

  isAuthenticated: false,
  isLoading: false,
  initialized: false,
  initializing: false,

  setUser(user) {
    set({
      user,
      isAuthenticated: true,
    });
  },

  login(data: any) {
    tokenStorage.setTokens(data.accessToken, data.refreshToken);

    set({
      isAuthenticated: true,
    });
  },

  logout() {
    set({
      user: undefined,
      permissions: [],
      isAuthenticated: false,
    });
  },

  initialize: async () => {
    console.log('INITIALIZE START');

    console.log('BEFORE', get().initialized, get().initializing);

    if (get().initialized || get().initializing) {
      return;
    }

    set({ initializing: true });

    try {
      const accessToken = tokenStorage.getAccessToken();

      console.log('ACCESS TOKEN', accessToken);
      if (!accessToken) {
        set({
          isAuthenticated: false,
          user: null,
        });
        return;
      }

      const user = await me();
      console.log('ME RESULT', user);
      set({
        user,
        permissions: user.permissions,
        isAuthenticated: true,
      });
      console.log('after set', get().isAuthenticated);
    } catch (error) {
      tokenStorage.clear();

      set({
        user: null,
        permissions: [],
        isAuthenticated: false,
      });
    } finally {
      set({
        isLoading: false,
        initialized: true,
        initializing: false,
      });

      console.log('FINALLY', get().initialized, get().initializing, get().isAuthenticated);
    }
  },
}));
