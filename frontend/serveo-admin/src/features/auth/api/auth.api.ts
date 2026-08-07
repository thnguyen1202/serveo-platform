import { apiClient } from '@/core/api/api.client';

import type { LoginRequest, LoginResponse } from '../types/auth.types';
import type { AuthUser } from '@/core/auth/auth.types';

export async function login(request: LoginRequest) {
  const response = await apiClient.post<LoginResponse>('/auth/login', request);

  return response.data;
}

export async function me() {
  const response = await apiClient.get<AuthUser>('/auth/me');

  return response.data;
}
