export interface DashboardSummary {
  totalTenants: number;
  activeTenants: number;
  totalBranches: number;
  totalUsers: number;
  todayOrders: number;
  todayRevenue: number;
}

export interface RevenuePoint {
  date: string;
  revenue: number;
}

export interface TenantGrowthPoint {
  month: string;
  count: number;
}

export interface ActivityItem {
  id: string;
  action: string;
  user: string;
  createdAt: string;
}

export interface SystemHealth {
  apiStatus: string;
  databaseStatus: string;
  queueStatus: string;
}
