import { type PropsWithChildren } from 'react';
import { useCrudState } from '@/context/crud-context';
import type { Tenant } from './entity';
import { TenantContext, type TenantsDialogType } from './context';

export function TenantsProvider(props: PropsWithChildren) {
  const value = useCrudState<Tenant, TenantsDialogType>();

  return <TenantContext value={value}>{props.children}</TenantContext>;
}
