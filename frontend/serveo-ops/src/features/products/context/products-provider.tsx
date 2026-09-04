import { type PropsWithChildren } from 'react';
import { useCrudState } from '@/shared/context/crud-context';
import { ProductsContext, type Product, type ProductsDialogType } from './products.context';

export function ProductsProvider(props: PropsWithChildren) {
  const value = useCrudState<Product, ProductsDialogType>();

  return <ProductsContext value={value}>{props.children}</ProductsContext>;
}
