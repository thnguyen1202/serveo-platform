import { Table, TableBody, TableCell, TableHead, TableHeader, TableRow } from '@/components/ui/table';
import { useState } from 'react';
import { usePagingQuery } from '../core/use-actions';
import { DataTableSkeleton } from '@/components/data-table/skeleton';
import { DataTableToolbar } from '@/components/data-table/toolbar';
import {
  flexRender,
  getCoreRowModel,
  useReactTable,
  type RowSelectionState,
  type SortingState,
  type VisibilityState,
} from '@tanstack/react-table';
import { columns } from './columns';
import { getRouteApi } from '@tanstack/react-router';
import { useTableUrlState } from '@/hooks/use-table-url-state';
import { cn } from '@/lib/utils';

export function TasksTable() {
  const [sorting, setSorting] = useState<SortingState>([]);
  const [columnVisibility, setColumnVisibility] = useState<VisibilityState>({});
  const [rowSelection, setRowSelection] = useState<RowSelectionState>({});

  const route = getRouteApi('/_authenticated/tenants/');
  const { globalFilter, onGlobalFilterChange, columnFilters, onColumnFiltersChange, pagination, onPaginationChange } =
    useTableUrlState({
      search: route.useSearch(),
      navigate: route.useNavigate(),
      pagination: { defaultPage: 1, defaultPageSize: 10 },
      globalFilter: { enabled: true, key: 'filter' },
      columnFilters: [
        { columnId: 'status', searchKey: 'status', type: 'array' },
        { columnId: 'priority', searchKey: 'priority', type: 'array' },
      ],
    });
  const { data, isPending } = usePagingQuery({
    page: pagination.pageIndex + 1,
    size: pagination.pageSize,
    sort: sorting.toString(),
  });
  const items = data?.items ?? [];
  const totalCount = data?.itemCount ?? 0;
  const pageCount = Math.ceil(totalCount / pagination.pageSize);

  const table = useReactTable({
    data: items,
    columns,

    state: {
      sorting,
      columnVisibility,
      rowSelection,
      columnFilters,
      globalFilter,
      pagination,
    },

    manualPagination: true,
    pageCount,

    onPaginationChange,
    onGlobalFilterChange,
    onColumnFiltersChange,

    onSortingChange: setSorting,
    onColumnVisibilityChange: setColumnVisibility,
    onRowSelectionChange: setRowSelection,

    getCoreRowModel: getCoreRowModel(),
  });

  if (isPending) return <DataTableSkeleton columns={6} rows={10} />;

  return (
    <div  className={cn(
        'max-sm:has-[div[role="toolbar"]]:mb-16', // Add margin bottom to the table on mobile when the toolbar is visible
        'flex flex-1 flex-col gap-4'
      )}>
      <DataTableToolbar
        table={table}
        searchKey="name"
        searchPlaceholder="Search tenant..."
      />
      <div className="overflow-hidden rounded-md border">
        <Table aria-label="Products" className="min-w-xl">
          <TableHeader>
            {table.getFlatHeaders().map((header) => (
              <TableHead key={header.id}>
                {header.isPlaceholder ? null : flexRender(header.column.columnDef.header, header.getContext())}
              </TableHead>
            ))}
          </TableHeader>
          <TableBody className="text-[0.85rem]">
            {table.getRowModel().rows?.length ? (
              table.getRowModel().rows.map((row) => (
                <TableRow key={row.id} data-state={row.getIsSelected() && 'selected'}>
                  {row.getVisibleCells().map((cell) => (
                    <TableCell key={cell.id}>{flexRender(cell.column.columnDef.cell, cell.getContext())}</TableCell>
                  ))}
                </TableRow>
              ))
            ) : (
              <TableRow>
                <TableCell colSpan={columns.length} className="h-24 text-center">
                  No results.
                </TableCell>
              </TableRow>
            )}
          </TableBody>
        </Table>
      </div>
    </div>
  );
}
