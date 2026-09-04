import type { CrudUIState } from '@/shared/context/crud-context';
import type { BaseDialogType } from '@/lib/base-dialog-type';
import { createContext } from 'react';
import z from 'zod';

const menuSchema = z.object({
  id: z.string(),
  name: z.string(),
});

export type Menu = z.infer<typeof menuSchema>;
export type MenusDialogType = BaseDialogType;

export const MenusContext = createContext<CrudUIState<Menu, MenusDialogType> | null>(null);
