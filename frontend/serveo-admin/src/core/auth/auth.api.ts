import { apiClient } from '@/core/api/api.client';

import { tokenStorage } from './token-storage';

export async function refreshToken() {
  console.log('Refreshing token...');
  const refresh = tokenStorage.getRefreshToken();

  if (!refresh) throw new Error('No refresh token');

  const response = await apiClient.post('/auth/refresh', {
    refreshToken: refresh,
  });

  tokenStorage.setTokens(response.data.accessToken, response.data.refreshToken);
  return response.data;
}

export async function me() {
  const response = await apiClient.get('/auth/me');

  return response.data;
}
