import type { CrudContextType } from '@/context/crud-context';
import type { BaseDialogType } from '@/lib/base-dialog-type';
import type { Tenant } from './entity';
import { createContext } from 'react';

export type TenantsDialogType = BaseDialogType;
export const TenantContext = createContext<CrudContextType<Tenant, TenantsDialogType> | null>(null);
