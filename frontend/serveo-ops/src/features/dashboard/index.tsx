import { Content } from '@/shared/components/layout/main';

export function Dashboard() {
  return (
    <>
      {/* ===== Top Heading ===== */}
      {/* <BaseHeader /> */}

      {/* ===== Main ===== */}
      <Content>
        <div className="mb-2 flex items-center justify-between space-y-2">
          <h1 className="text-2xl font-bold tracking-tight">Dashboard</h1>
        </div>
      </Content>
    </>
  );
}
