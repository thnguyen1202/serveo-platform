import type { ColumnDef } from '@tanstack/react-table';
import type { Tenant } from '../core/entity';

export const columns: ColumnDef<Tenant>[] = [
  {
    accessorKey: 'name',
    meta: { label: 'Name' },
    header: 'Name',
  },
  {
    accessorKey: 'expiredTime',
    meta: { label: 'Expired time' },
    header: 'Expired Time',
  },
  {
    accessorKey: 'status',
    meta: { label: 'Status' },
    header: 'Status',
  },
  // {
  //   accessorKey: 'createdAt',
  //   meta: { label: 'Created date' },
  //   header: 'Created date',
  // },
];
