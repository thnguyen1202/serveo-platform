import { apiClient } from '@/lib/axios/api.client';
import type { AxiosRequestConfig } from 'axios';
import type { PagedResult } from '@/lib/paging';
import type { Table, TableCreateRequest } from './tables.schema';

// get paged
export async function getPagedTable(config?: AxiosRequestConfig) {
  const response = await apiClient.get<PagedResult<Table>>('/tables', config);
  return response.data;
}

// create
export async function createTable(request: TableCreateRequest) {
  const response = await apiClient.post('/tables', request);
  return response.data;
}
