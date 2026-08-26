import { apiClient } from '@/core/api/api.client';
import type { AxiosRequestConfig } from 'axios';

export async function getMenuOptions(config?: AxiosRequestConfig) {
  const { data } = await apiClient.get<MenuOptionResponse[]>('/me/menus', config);
  return data;
}

export interface MenuOptionResponse {
  id: string;
  name: string;
  itemCount: number;
}