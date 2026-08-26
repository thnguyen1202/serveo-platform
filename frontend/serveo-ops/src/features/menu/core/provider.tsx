import { type PropsWithChildren } from 'react';
import { useCrudState } from '@/context/crud-context';
import type { Menu } from './entity';
import { MenuContext, type MenusDialogType } from './context';

export function MenusProvider(props: PropsWithChildren) {
  const value = useCrudState<Menu, MenusDialogType>();

  return <MenuContext value={value}>{props.children}</MenuContext>;
}
