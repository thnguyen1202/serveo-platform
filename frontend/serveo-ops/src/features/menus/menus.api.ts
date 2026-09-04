import { apiClient } from '@/lib/axios/api.client';
import type { MenuCreateRequest, MenuCreateResponse } from './menus.types';


export async function createMenu(request: MenuCreateRequest) {
  const response = await apiClient.post<MenuCreateResponse>('/menus', request);
  return response.data;
}
