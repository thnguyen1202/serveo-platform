import { PageHeader } from '@/components/common/page-header';

import { ActivityFeed } from '../components/activity-feed';
import { KpiSection } from '../components/kpi-section';
import { RevenueChart } from '../components/revenue-chart';

export default function DashboardPage() {
  return (
    <div className="space-y-6">
      <PageHeader title="Dashboard" description="System overview" />

      <KpiSection />

      <div className="grid grid-cols-2 gap-6">
        <RevenueChart />

        {/* <TenantChart /> */}
      </div>

      <ActivityFeed />

      {/* <SystemHealth /> */}
    </div>
  );
}
