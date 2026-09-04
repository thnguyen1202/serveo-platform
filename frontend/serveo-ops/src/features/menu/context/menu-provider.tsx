import { useState, type PropsWithChildren } from 'react';
import { useMenusContext } from '@/features/menus/context/use-menus-context';
import { useCategoriesContext } from '@/features/categories/context/use-categories-context';
import { useProductsContext } from '@/features/products/context/use-products-context';
import { MenuContext, type MenuContextValue } from './menu.context';
import type { Menu } from '../menu.schema';
import type { Category } from '@/features/categories/context/categories.context';
import { CategoriesProvider } from '@/features/categories/context/categories-provider';
import { ProductsProvider } from '@/features/products/context/products-provider';
import { MenusProvider } from '@/features/menus/context/menus-provider';

function MenuContextProvider(props: PropsWithChildren) {
  const menus = useMenusContext();
  const categories = useCategoriesContext();
  const products = useProductsContext();

  const [selectedMenu, setSelectedMenu] = useState<Menu | null>(null);
  const [selectedCategory, setSelectedCategory] = useState<Category | null>(null);

  const value: MenuContextValue = {
    menus,
    categories,
    products,

    selectedMenu,
    setSelectedMenu,

    selectedCategory,
    setSelectedCategory,
  };

  return <MenuContext.Provider value={value}>{props.children}</MenuContext.Provider>;
}

export function MenuProvider(props: PropsWithChildren) {
  return (
    <MenusProvider>
      <CategoriesProvider>
        <ProductsProvider>
          <MenuContextProvider>
            {props.children}
          </MenuContextProvider>
        </ProductsProvider>
      </CategoriesProvider>
    </MenusProvider>
  );
}