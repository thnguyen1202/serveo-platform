import { useMutation, useQueryClient } from '@tanstack/react-query';
import { createCategoryForMenu, createMenu, createProductForMenu } from './menu.api';
import type { CategoryCreateRequest, ProductCreateRequest } from './menu.types';

export function useCreateMenu() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: createMenu,

    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['menus'] });
    },
  });
}


export function useCreateCategory() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: ({ menuId, data }: {
      menuId: string;
      data: CategoryCreateRequest;
    }) => createCategoryForMenu(menuId, data),

    onSuccess: (_, variables) => {
      // Làm mới danh sách categories chung
      //queryClient.invalidateQueries({ queryKey: ['categories'] });
      
      // (Tùy chọn) Làm mới chính xác danh sách categories của menu hiện tại
      //queryClient.invalidateQueries({ queryKey: ['menus', variables.menuId, 'categories'] });

      queryClient.invalidateQueries({ queryKey: ['menus', variables.menuId] });
    },
  });
}


export function useCreateProduct() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: ({ menuId, data }: {
      menuId: string;
      data: ProductCreateRequest;
    }) => createProductForMenu(menuId, data),

    onSuccess: (_, variables) => {
      queryClient.invalidateQueries({ queryKey: ['menus', variables.menuId] });
    },
  });
}