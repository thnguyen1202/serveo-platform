import { type PropsWithChildren } from 'react';
import { useCrudState } from '@/shared/context/crud-context';
import { CategoriesContext, type Category, type CategoriesDialogType } from './categories.context';

export function CategoriesProvider(props: PropsWithChildren) {
  const value = useCrudState<Category, CategoriesDialogType>();

  return <CategoriesContext value={value}>{props.children}</CategoriesContext>;
}
