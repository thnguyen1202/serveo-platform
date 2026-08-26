// import { useAuthStore } from '@/stores/auth-store'
import { ConfirmDialog } from '@/components/confirm-dialog';
import { useAuthStore } from '@/core/auth/auth.store';
import { useNavigate } from '@tanstack/react-router';
import { useTranslation } from 'react-i18next';

interface SignOutDialogProps {
  isOpen: boolean;
  onOpenChange: (isOpen: boolean) => void;
}

export function LogoutDialog({ isOpen, onOpenChange }: SignOutDialogProps) {
  const { t } = useTranslation('common');
  const navigate = useNavigate();
  const { clear: logout } = useAuthStore();

  const handleLogout = () => {
    logout();

    // Preserve current location for redirect after sign-in
    const currentPath = location.pathname;
    navigate({
      to: '/login',
      search: { redirect: currentPath },
      replace: true,
    });
  };

  return (
    <ConfirmDialog
      isOpen={isOpen}
      onOpenChange={onOpenChange}
      title={t('logout.title')}
      desc={t('logout.description')}
      confirmText={t('logout.title')}
      destructive
      handleConfirm={handleLogout}
      className="sm:max-w-sm"
      isLoading={false}
      disabled={false}
    />
  );
}
