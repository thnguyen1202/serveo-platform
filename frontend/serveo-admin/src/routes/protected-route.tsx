import { Navigate } from 'react-router-dom';

import { useAuthStore } from '@/core/auth/auth.store';

export default function ProtectedRoute({ children }: { children: React.ReactNode }) {
  const { isAuthenticated, isLoading } = useAuthStore();

  if (isLoading) {
    return <div>Loading...</div>;
  }

  if (!isAuthenticated) {
    return <Navigate to="/login" />;
  }

  return children;
}

// import { Navigate } from 'react-router-dom';

// import { useAuthStore } from '@/core/auth/auth.store';

// export default function ProtectedRoute({ children }: { children: React.ReactNode }) {
//   const authenticated = useAuthStore((state) => state.isAuthenticated);

//   if (!authenticated) {
//     return <Navigate to="/login" />;
//   }

//   return children;
// }
