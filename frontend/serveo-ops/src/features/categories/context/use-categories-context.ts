import { useContext } from 'react';
import { CategoriesContext } from './categories.context';

export function useCategoriesContext() {
  const context = useContext(CategoriesContext);

  if (!context) {
    const name = useCategoriesContext.name;
    throw new Error(`${name} must be used within its Provider.`);
  }

  return context;
}
