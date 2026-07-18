import { tokenStorage } from '@/core/auth/token-storage';
import { env } from '@/core/config/env';

import { apiClient } from './api-client';
import { handleHttpError } from './handle-http-error';

export function setupRequestInterceptor() {
  apiClient.interceptors.request.use((config) => {
    const url = config.url ?? '';

    // login endpoint
    if (url.includes('/auth/login')) {
      config.headers['X-Api-Key'] = env.apiKey;
    } else {
      const token = tokenStorage.getAccessToken();

      if (token) {
        config.headers.Authorization = `Bearer ${token}`;
      }
    }

    return config;
  });
}

apiClient.interceptors.response.use(
  (response) => response,

  (error) => {
    return Promise.reject(handleHttpError(error));
    // if (axios.isAxiosError(error)) {
    //   const problem = error.response?.data;
    //   if (problem?.status) {
    //     return Promise.reject(new ApiException(problem));
    //   }
    // }
    // return Promise.reject(error);
  },
);

// apiClient.interceptors.response.use(
//   (response) => response,

//   async (error) => {
//     if (error.response.status === 401) {
//       const token = await refreshToken();

//       error.config.headers.Authorization = `Bearer ${token}`;

//       return apiClient(error.config);
//     }

//     return Promise.reject(error);
//   },
// );
