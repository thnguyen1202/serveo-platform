import useDialogState from '@/hooks/use-dialog-state';
import React from 'react';
import { createContext, useContext, useState } from 'react';

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

// export function createCrudContext<TEntity, TDialog extends string>() {
//   type ContextType = {
//     open: TDialog | null;
//     setOpen: (value: TDialog | null) => void;
//     currentRow: TEntity | null;
//     setCurrentRow: React.Dispatch<React.SetStateAction<TEntity | null>>;
//   };

//   const Context = createContext<ContextType | null>(null);

//   function Provider({ children }: { children: React.ReactNode }) {
//     const [open, setOpen] = useDialogState<TDialog>(null);

//     const [currentRow, setCurrentRow] = useState<TEntity | null>(null);

//     return (
//       <Context
//         value={{
//           open,
//           setOpen,
//           currentRow,
//           setCurrentRow,
//         }}
//       >
//         {children}
//       </Context>
//     );
//   }

//   function useCrud() {
//     const context = useContext(Context);

//     if (!context) {
//       throw new Error('Hook must be used within Provider');
//     }

//     return context;
//   }

//   return {
//     Provider,
//     useCrud,
//   };
// }
