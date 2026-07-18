import { useAuthStore } from '@/core/auth/auth.store';

export function useAuth() {
  const user = useAuthStore((state) => state.user);
  const logout = useAuthStore((state) => state.logout);

  return {
    user,
    logout,
    permissions: user?.permissions ?? [],
  };
}
