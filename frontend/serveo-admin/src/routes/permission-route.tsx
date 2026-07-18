// ======================
// Route Permission Guard
// ======================

import { Navigate } from 'react-router-dom';

import { useAuth } from '@/features/auth/hooks/use-auth';

export default function PermissionRoute({ permission, children }: { permission: string; children: React.ReactNode }) {
  const { permissions } = useAuth();

  if (!permissions.includes(permission)) {
    return <Navigate to="/403" />;
  }

  return children;
}
