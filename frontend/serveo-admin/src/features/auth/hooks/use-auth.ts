import { useAuthStore } from '@/core/auth/auth.store';

export function useAuth() {
  const user = useAuthStore((state) => state.user);
  const logout = useAuthStore((state) => state.logout);
  // const setUser = useAuthStore(s => s.setUser);

  return {
    user,
    permissions: user?.permissions ?? [],
    isAuthenticated: !!user,
    isLoading: useAuthStore((state) => state.isLoading),

    logout,
    // setUser,
    // initialize: useAuthStore((state) => state.initialize),
  };
}
