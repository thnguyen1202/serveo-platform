import type { CrudUIState } from '@/shared/context/crud-context';
import { createContext, type Dispatch, type SetStateAction } from 'react';
import type { Menu, MenusDialogType } from '@/features/menus/context/menus.context';
import type { CategoriesDialogType, Category } from '@/features/categories/context/categories.context';
import type { Product, ProductsDialogType } from '@/features/products/context/products.context';

export interface MenuContextValue {
  menus: CrudUIState<Menu, MenusDialogType>;
  categories: CrudUIState<Category, CategoriesDialogType>;
  products: CrudUIState<Product, ProductsDialogType>;

  selectedMenu: Menu | null;
  setSelectedMenu: Dispatch<SetStateAction<Menu | null>>;

  selectedCategory: Category | null;
  setSelectedCategory: Dispatch<SetStateAction<Category | null>>;
}

export const MenuContext = createContext<MenuContextValue | null>(null);
