import useDialogState from '@/hooks/use-dialog-state';
import { useState } from 'react';

export type CrudState<TEntity, TDialog extends string> = {
  open: TDialog | null;
  setOpen: (value: TDialog | null) => void;
  currentRow: TEntity | null;
  setCurrentRow: React.Dispatch<React.SetStateAction<TEntity | null>>;
};

export function useCrudState<TEntity, TDialog extends string>(): CrudState<TEntity, TDialog> {
  const [open, setOpen] = useDialogState<TDialog>(null);
  const [currentRow, setCurrentRow] = useState<TEntity | null>(null);

  return {
    open,
    setOpen,
    currentRow,
    setCurrentRow,
  };
}
