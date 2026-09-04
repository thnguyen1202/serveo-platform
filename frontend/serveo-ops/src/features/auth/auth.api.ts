import { apiClient } from '@/lib/axios/api.client';
import type { LoginRequest, LoginResponse } from './auth.types';

export async function login(request: LoginRequest) {
  const response = await apiClient.post<LoginResponse>('/auth/login', request);

  return response.data;
}
