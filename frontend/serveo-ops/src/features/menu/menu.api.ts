import { apiClient } from '@/lib/axios/api.client';
import type { AxiosRequestConfig } from 'axios';
import type { PagedResult } from '@/lib/paging';
import type { Menu } from './menu.schema';
import type { CategoryCreateRequest, MenuCreateRequest, MenuCreateResponse, MenuOptionResponse, ProductCreateRequest } from './menu.types';

export async function getMenuOptions(config?: AxiosRequestConfig) {
  const { data } = await apiClient.get<MenuOptionResponse[]>('/me/menus', config);
  return data;
}

export async function getPaged(config?: AxiosRequestConfig) {
  const response = await apiClient.get<PagedResult<Menu>>('/menus', config);
  return response.data;
}

export async function createMenu(request: MenuCreateRequest) {
  const response = await apiClient.post<MenuCreateResponse>('/menus', request);
  return response.data;
}

// Tạo mới một Category trong Menu
export async function createCategoryForMenu(menuId: string, data: CategoryCreateRequest) {
  const response = await apiClient.post(`/menus/${menuId}/categories`, data);
  return response.data;
}

// Tạo mới một Product trong Menu
export async function createProductForMenu(menuId: string, data: ProductCreateRequest) {
  const response = await apiClient.post(`/menus/${menuId}/products`, data);
  return response.data;
}

// get menu details
export async function getMenuDetails(menuId: string, full: boolean, config?: AxiosRequestConfig) {
  const response = await apiClient.get(`/menus/${menuId}`, {
    ...config,
    params: {
      ...config?.params,
      full,
    },
  });
  
  return response.data;
}


//Gán (Attach) danh sách Category có sẵn vào Menu
//Gỡ / Xóa 1 Category khỏi Menu (Single Detach / Delete)
//Gỡ hàng loạt Category khỏi Menu (Bulk Detach)
// const createCategoryForMenu = async (menuId, categoryData) => {
//   try {
//     const response = await api.post(`/api/menus/${menuId}/categories`, {
//       name: categoryData.name,
//       description: categoryData.description,
//       displayOrder: categoryData.displayOrder,
//       isActive: true,
//     });
    
//     console.log('Category created:', response.data);
//     return response.data;
//   } catch (error) {
//     handleApiError(error);
//   }
// };

// // Gọi hàm
// createCategoryForMenu('m-101', { name: 'Món khai vị', displayOrder: 1 });

// const attachCategoriesToMenu = async (menuId, categoryIds) => {
//   try {
//     const response = await api.post(`/api/menus/${menuId}/categories`, {
//       categoryIds: categoryIds, // e.g., ['cat-1', 'cat-2']
//     });

//     console.log('Categories attached:', response.data);
//     return response.data;
//   } catch (error) {
//     handleApiError(error);
//   }
// };

// // Gọi hàm
// attachCategoriesToMenu('m-101', ['cat-501', 'cat-502']);

// const removeCategoryFromMenu = async (menuId, categoryId) => {
//   try {
//     const response = await api.delete(`/api/menus/${menuId}/categories/${categoryId}`);
    
//     console.log('Category removed:', response.data);
//     return response.data;
//   } catch (error) {
//     handleApiError(error);
//   }
// };

// // Gọi hàm
// removeCategoryFromMenu('m-101', 'cat-501');

// const bulkDetachCategories = async (menuId, categoryIds) => {
//   try {
//     const response = await api.post(`/api/menus/${menuId}/categories/detach`, {
//       categoryIds: categoryIds,
//     });

//     return response.data;
//   } catch (error) {
//     handleApiError(error);
//   }
// };