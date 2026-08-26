import { apiClient } from '@/core/api/api.client';

import type { LoginRequest, LoginResponse } from '../types/auth.types';

export async function login(request: LoginRequest) {
  const response = await apiClient.post<LoginResponse>('/auth/login', request);

  return response.data;
}
