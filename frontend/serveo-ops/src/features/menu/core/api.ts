import { apiClient } from '@/core/api/api.client';

import type { CreateRequest, CreateResponse } from './types';
import type { Menu } from './entity';
import type { PagedResult } from '@/lib/paging';

export async function getPaged() {
  const response = await apiClient.get<PagedResult<Menu>>('/menus');

  return response.data;
}

export async function create(request: CreateRequest) {
  const response = await apiClient.post<CreateResponse>('/menus', request);

  return response.data;
}
