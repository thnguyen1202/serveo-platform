import { useContext } from 'react';
import { MenuContext } from './menu.context';

export function useMenuContext() {
  const context = useContext(MenuContext);

  if (!context) {
    const name = useMenuContext.name;
    throw new Error(`${name} must be used within its Provider.`);
  }

  return context;
}
