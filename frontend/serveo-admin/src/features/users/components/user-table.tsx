import { DataTable } from '@/components/common/data-table/data-table';

import { columns } from './user-columns';

export function UserTable({ data }) {
  return <DataTable columns={columns} data={data} />;
}
