import { env } from '@/core/config/env';
import axios from 'axios';
import { tokenStorage } from '../auth/token-storage';
import { refreshToken } from '../auth/auth.api';
import { ApiException } from './api.exception';

export const apiClient = axios.create({
  baseURL: env.apiUrl,
  // timeout: 15000,
  headers: {
    'Content-Type': 'application/json',
  },
});

let refreshPromise: Promise<void> | null = null;

apiClient.interceptors.request.use(async (config) => {
  const url = config.url ?? '';
  const isAuthEndpoint = url.includes('/auth/login') || url.includes('/auth/refresh');
  if (isAuthEndpoint) {
    config.headers['X-Api-Key'] = env.apiKey;
    return config;
  }

  let accessToken = tokenStorage.getAccessToken();
  if (accessToken) {
    if (tokenStorage.isExpiringSoon()) {
      if (!refreshPromise) {
        refreshPromise = refreshToken()
          .catch((error) => {
            tokenStorage.clear();
            window.location.href = '/login';

            throw error;
          })
          .finally(() => {
            refreshPromise = null;
          });
      }

      await refreshPromise;
      accessToken = tokenStorage.getAccessToken(); // lấy token mới
    }

    if (accessToken) {
      config.headers.Authorization = `Bearer ${accessToken}`;
    }
  }

  return config;
});

apiClient.interceptors.response.use(
  (response) => response,
  async (error) => {
    console.dir('apiClient:interceptors.response:error', error);
    if (error.response?.status === 401) {
      if (!refreshPromise) {
        console.log('apiClient.interceptors.response.use', 'error 401, refreshing...');
        refreshPromise = refreshToken().finally(() => {
          refreshPromise = null;
        });
      }

      await refreshPromise;
    }
    return Promise.reject(handleHttpError(error));
  },
);

function handleHttpError(error: unknown) {
  if (import.meta.env.VITE_ENABLE_LOG === 'true') {
    console.dir('handleHttpError', error);
  }

  if (axios.isAxiosError(error)) {
    const problem = error.response?.data;
    if (problem) {
      return new ApiException(problem, error.status);
    }
  }

  return error;
}
