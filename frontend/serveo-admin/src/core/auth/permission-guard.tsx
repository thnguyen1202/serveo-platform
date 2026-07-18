import { useAuth } from '@/features/auth/hooks/use-auth';

interface Props {
  permission: string;

  children: React.ReactNode;
}

export default function PermissionGuard({ permission, children }: Props) {
  const { permissions } = useAuth();

  if (!permissions.includes(permission)) {
    return null;
  }

  return children;
}
