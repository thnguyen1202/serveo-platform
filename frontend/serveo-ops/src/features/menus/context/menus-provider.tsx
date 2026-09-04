import { type PropsWithChildren } from 'react';
import { useCrudState } from '@/shared/context/crud-context';
import { MenusContext, type Menu, type MenusDialogType } from './menus.context';

export function MenusProvider(props: PropsWithChildren) {
  const value = useCrudState<Menu, MenusDialogType>();

  return <MenusContext.Provider value={value}>{props.children}</MenusContext.Provider>;
}
