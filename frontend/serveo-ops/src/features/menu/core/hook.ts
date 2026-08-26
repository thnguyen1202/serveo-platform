import { useContext } from 'react';
import { MenuContext } from './context';

export function useMenus() {
  const context = useContext(MenuContext);

  if (!context) {
    const name = useMenus.name;
    throw new Error(`${name} must be used within its Provider.`);
  }

  return context;
}
