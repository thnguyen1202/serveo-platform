import { env } from '@/shared/config/env';
import axios from 'axios';
import { ApiException } from './api.exception';
import { refreshToken } from '@/app/bootstrap/auth.api';
import { useAuthStore } from '@/app/bootstrap/auth.store';
import type { AuthTokenResponse } from '@/app/bootstrap/auth.types';

export const apiClient = axios.create({
  baseURL: env.apiUrl,
  withCredentials: true, // QUAN TRỌNG: Cho phép gửi và nhận HTTP-Only Cookie
  timeout: 10000, // 10 giây cho tất cả các API thông thường
  headers: {
    'Content-Type': 'application/json',
  },
});

// const { t } = useTranslation('common');
const csrfProtectedEndpoints = ['/auth/refresh', '/auth/logout'];
const aipkeyProtectedEndpoints = ['/auth/refresh', '/auth/login', '/auth/csrf'];
let refreshPromise: Promise<AuthTokenResponse> | null = null;

apiClient.interceptors.request.use(async (config) => {
  const url = config.url ?? '';
  const authStore = useAuthStore.getState();

  const requiresCsrf = csrfProtectedEndpoints.some((endpoint) => url.startsWith(endpoint));
  if (requiresCsrf && authStore.csrfToken) {
    config.headers['X-CSRF-TOKEN'] = authStore.csrfToken;
  }

  const requiresApikey = aipkeyProtectedEndpoints.some((endpoint) => url.startsWith(endpoint));
  if (requiresApikey) {
    config.headers['X-API-KEY'] = env.apiKey;
  }

  if (authStore.accessToken) {
    config.headers.Authorization = `Bearer ${authStore.accessToken}`;
  }

  return config;
});

apiClient.interceptors.response.use(
  (response) => response,
  async (error) => {
    console.dir('apiClient:interceptors.response:error', error);

    // Kiểm tra nếu lỗi là do Timeout
    if (error.code === 'ECONNABORTED' && error.message.includes('timeout')) {
      // alert(t('error.request.timeout'));
      alert('Network connection is too slow or the server is not responding. Please try again later!');
    }

    if (axios.isCancel(error)) {
      return Promise.reject(error);
    }

    const url = error.config.url ?? '';
    const isAuthEndpoint = url.includes('/auth/login') || url.includes('/auth/refresh');
    if (!isAuthEndpoint && error.response?.status === 401) {
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
