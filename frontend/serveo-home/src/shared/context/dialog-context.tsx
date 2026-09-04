import { createContext, useContext, useState, type PropsWithChildren } from "react";

interface DialogPayloadMap {
  "menu-create": undefined;
  "menu-edit": undefined;

  "category-create": { menuId?: string };
  "category-edit": undefined;

  "product-create": { menuId: string; categoryId: string };
  "product-edit": undefined;

  "table-create": undefined;
  "table-edit": undefined;
}

type DialogType = keyof DialogPayloadMap;
type DialogState = {
  [T in DialogType]: {
    type: T;
    payload: DialogPayloadMap[T];
  };
}[DialogType];

interface DialogContextType {
  dialog: DialogState | null;

  openDialog<T extends DialogType>(dialog: Extract<DialogState, { type: T }>): void;

  closeDialog(): void;
}

const DialogContext = createContext<DialogContextType | null>(null);

export function DialogProvider(props: PropsWithChildren) {
  const [dialog, setDialog] = useState<DialogState | null>(null);

  function openDialog(dialog: DialogState) {
    setDialog(dialog);
  }

  function closeDialog() {
    setDialog(null);
  }

  return (
    <DialogContext.Provider
      value={{
        dialog,
        openDialog,
        closeDialog,
      }}
    >
      {props.children}
    </DialogContext.Provider>
  );
}

export function useDialog() {
  const context = useContext(DialogContext);

  if (!context) {
    throw new Error("useDialog must be used within DialogProvider");
  }

  return context;
}
