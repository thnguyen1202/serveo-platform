import axios from 'axios';

import { env } from '@/core/config/env';

export const apiClient = axios.create({
  baseURL: env.apiUrl,
  timeout: 15000,
  headers: {
    'Content-Type': 'application/json',
  },
});
