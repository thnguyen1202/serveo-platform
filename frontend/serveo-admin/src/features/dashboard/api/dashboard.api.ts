import { apiClient } from '@/core/http/api-client';

import type { DashboardSummary } from '../types/dashboard.types';

export async function getDashboardSummary() {
  const response = await apiClient.get<DashboardSummary>('/admin/dashboard/summary');

  return response.data;
}
