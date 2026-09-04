import { useContext } from 'react';
import { MenusContext } from './menus.context';

export function useMenusContext() {
  const context = useContext(MenusContext);

  if (!context) {
    const name = useMenusContext.name;
    throw new Error(`${name} must be used within its Provider.`);
  }

  return context;
}
