import { useDashboardSummary } from '../hooks/use-dashboard';
import { KpiCard } from './kpi-card';

export function KpiSection() {
  const { data } = useDashboardSummary();

  return (
    <div
      className="
grid
grid-cols-4
gap-4
"
    >
      <KpiCard
        title="Tenants"

        value={data?.totalTenants ?? 0}
      />

      <KpiCard
        title="Branches"

        value={data?.totalBranches ?? 0}
      />

      <KpiCard
        title="Users"

        value={data?.totalUsers ?? 0}
      />

      <KpiCard
        title="Orders Today"

        value={data?.todayOrders ?? 0}
      />
    </div>
  );
}
