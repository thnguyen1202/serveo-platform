import useDialogState from '@/shared/hooks/use-dialog-state';
import React from 'react';
import { useState } from 'react';

type CrudContextType<TEntity, TDialog extends string> = {
  open: TDialog | null;
  setOpen: (value: TDialog | null) => void;
  currentRow: TEntity | null;
  setCurrentRow: React.Dispatch<React.SetStateAction<TEntity | null>>;
};

export function createCrudContext<TEntity, TDialog extends string>() {
  return React.createContext<CrudContextType<TEntity, TDialog> | null>(null);
}

export function useCrudState<TEntity, TDialog extends string>() {
  const [open, setOpen] = useDialogState<TDialog>(null);

  const [currentRow, setCurrentRow] = useState<TEntity | null>(null);

  return {
    open,
    setOpen,
    currentRow,
    setCurrentRow,
  };
}
