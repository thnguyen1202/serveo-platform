import useDialogState from '@/hooks/use-dialog-state';
import { createContext, useContext, useState } from 'react';

export type CrudContextType<TEntity, TDialog extends string> = {
  open: TDialog | null;
  setOpen: (dialog: TDialog | null) => void;
  currentRow: TEntity | null;
  setCurrentRow: React.Dispatch<React.SetStateAction<TEntity | null>>;
};

export function createCrudContext<TEntity, TDialog extends string>() {
  const Context = createContext<CrudContextType<TEntity, TDialog> | null>(null);

  function useCrud() {
    const value = useContext(Context);

    if (!value) {
      throw new Error('Hook must be used within Provider');
    }

    return value;
  }

  return {
    Context,
    useCrud,
  };
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
