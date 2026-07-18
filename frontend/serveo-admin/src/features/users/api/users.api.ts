/** API Service */
import { apiClient } from '@/core/http/api-client';
import type { PagedResult } from '@/types/paging';

import type { CreateUserRequest, User, UserPageRequest } from '../types/user.types';

export async function getUsers(filter: UserPageRequest) {
  const response = await apiClient.get<PagedResult<User>>('/users', { params: filter });

  return response.data;
}

export async function createUser(data: CreateUserRequest) {
  const response = await apiClient.post('/users', data);

  return response.data;
}

export async function updateUser(id: string, data: unknown) {
  return apiClient.put(`/users/${id}`, data);
}

export async function deleteUser(id: string) {
  return apiClient.delete(`/users/${id}`);
}

export async function assignRole(userId: string, roleIds: string[]) {
  return apiClient.post(`/users/${userId}/roles`, {
    roleIds,
  });
}
