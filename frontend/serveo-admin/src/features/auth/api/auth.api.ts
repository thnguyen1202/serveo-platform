import { apiClient } from '@/core/http/api-client';

import type { LoginRequest, LoginResponse, UserInfo } from '../types/auth.types';

export async function login(request: LoginRequest) {
  const response = await apiClient.post<LoginResponse>('/auth/login', request);

  return response.data;
}

// export async function login(request: LoginRequest): Promise<LoginResponse> {
//   const response = await api.post<LoginResponse>('/auth/login', request);

//   return response.data;
// }

export async function getCurrentUser() {
  const response = await apiClient.get<UserInfo>('/auth/me');

  return response.data;
}
