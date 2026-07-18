import { apiClient } from '@/core/http/api-client';

import { tokenStorage } from './token-storage';

export async function refreshToken() {
  const refresh = tokenStorage.getRefreshToken();

  if (!refresh) throw new Error('No refresh token');

  const response = await apiClient.post('/auth/refresh', {
    refreshToken: refresh,
  });

  tokenStorage.setAccessToken(response.data.accessToken);

  return response.data.accessToken;
}
