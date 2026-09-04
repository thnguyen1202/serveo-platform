import { cn } from '@/lib/utils';
import {
  AlertDialog,
  AlertDialogCancel,
  AlertDialogDescription,
  AlertDialogFooter,
  AlertDialogHeader,
  AlertDialogTitle,
} from '@/shared/components/ui/alert-dialog';
import { Button } from '@/shared/components/ui/button';
import { useTranslation } from 'react-i18next';

type ConfirmDialogProps = {
  isOpen: boolean;
  onOpenChange: (isOpen: boolean) => void;
  title: React.ReactNode;
  disabled: boolean | false;
  desc: React.JSX.Element | string;
  cancelBtnText?: string;
  confirmText?: React.ReactNode;
  destructive?: boolean;
  isLoading: boolean | false;
  className?: string;
  children?: React.ReactNode;
} & ({ form: string; handleConfirm?: undefined } | { form?: undefined; handleConfirm: () => void });

export function ConfirmDialog(props: ConfirmDialogProps) {
  const { t } = useTranslation('common');
  const {
    title,
    desc,
    children,
    className,
    confirmText,
    cancelBtnText,
    destructive,
    isLoading,
    disabled,
    form,
    handleConfirm,
    ...actions
  } = props;
  return (
    <>
      <AlertDialog {...actions} className={cn(className)}>
        <AlertDialogHeader>
          <AlertDialogTitle>{title}</AlertDialogTitle>
          <AlertDialogDescription className="text-pretty">{desc}</AlertDialogDescription>
        </AlertDialogHeader>
        {children}
        <AlertDialogFooter>
          <AlertDialogCancel isDisabled={disabled}>{cancelBtnText ?? t('cancel')}</AlertDialogCancel>
          <Button
            type={form ? 'submit' : 'button'}
            form={form}
            onClick={handleConfirm}
            variant={destructive ? 'destructive' : 'default'}
            isDisabled={disabled || isLoading}
          >
            {confirmText ?? 'Continue'}
          </Button>
          {/* <AlertDialogAction>{confirmText ?? 'Continue'}</AlertDialogAction> */}
        </AlertDialogFooter>
      </AlertDialog>
    </>
  );
}
