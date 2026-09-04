import { useContext } from 'react';
import { ProductsContext } from './products.context';

export function useProductsContext() {
  const context = useContext(ProductsContext);

  if (!context) {
    const name = useProductsContext.name;
    throw new Error(`${name} must be used within its Provider.`);
  }

  return context;
}
