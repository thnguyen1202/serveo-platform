import { apiClient } from '@/lib/axios/api.client';
import type { AuthTokenResponse, AuthUserResponse, CsrfResponse } from './auth.types';
import { deviceStorage } from '@/shared/config/device.storage';
import type { AxiosRequestConfig } from 'axios';

export async function refreshToken(options: AxiosRequestConfig = {}) {
  const response = await apiClient.post<AuthTokenResponse>(
    '/auth/refresh',
    {
      deviceId: deviceStorage.getDeviceId(),
      clientType: deviceStorage.getClientType(),
    },
    options,
  );

  return response.data;
}

export async function csrfToken(options: AxiosRequestConfig = {}) {
  const response = await apiClient.get<CsrfResponse>('/auth/csrf', options);

  return response.data;
}

export async function me(options: AxiosRequestConfig = {}) {
  const response = await apiClient.get<AuthUserResponse>('/auth/me', options);

  return response.data;
}
