import type { CrudContextType } from '@/context/crud-context';
import type { BaseDialogType } from '@/lib/base-dialog-type';
import type { Menu } from './entity';
import { createContext } from 'react';

export type MenusDialogType = BaseDialogType;
export const MenuContext = createContext<CrudContextType<Menu, MenusDialogType> | null>(null);
