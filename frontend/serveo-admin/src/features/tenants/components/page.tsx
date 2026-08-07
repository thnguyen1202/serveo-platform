import { Main } from '@/components/layout/main';
import { TenantsProvider as OutletProvider } from '../core/provider';
import { TenantsPrimaryButtons as PrimaryButtons } from './primary-buttons';
import { TenantsDialogs as Dialogs } from './dialogs';
import { BaseHeader as Header } from '@/lib/base-header';
import { TasksTable } from './table';

export function Tenants() {
  return (
    <OutletProvider>
      <Header />

      <Main className="flex flex-1 flex-col gap-4 sm:gap-6">
        <div className="flex flex-wrap items-end justify-between gap-2">
          <div>
            <h2 className="text-2xl font-bold tracking-tight">Tenants</h2>
            <p className="text-muted-foreground">Manage your tenants here.</p>
          </div>
          <PrimaryButtons />
        </div>
        <TasksTable />
      </Main>

      <Dialogs />
    </OutletProvider>
  );
}
