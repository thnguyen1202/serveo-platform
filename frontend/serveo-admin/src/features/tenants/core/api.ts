import { apiClient } from '@/core/api/api.client';

import type { CreateRequest, CreateResponse } from './types';
import type { Tenant } from './entity';
import type { PagedResult } from '@/lib/paging';

export async function getPaged() {
  const response = await apiClient.get<PagedResult<Tenant>>('/tenants');

  return response.data;
}

export async function create(request: CreateRequest) {
  const response = await apiClient.post<CreateResponse>('/tenants', request);

  return response.data;
}
