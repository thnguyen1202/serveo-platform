import { cn, getPageNumbers } from '@/lib/utils';
import type { Table } from '@tanstack/react-table';
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from '@/components/ui/select';
import { Button } from '@/components/ui/button';
import { ChevronLeft, ChevronRight, ChevronsLeft, ChevronsRight } from 'lucide-react';

type DataTablePaginationProps<TData> = {
  table: Table<TData>;
  className?: string;
};

const PAGE_SIZES = [10, 20, 30, 40, 50];

export function DataTablePagination<TData>({ table, className }: DataTablePaginationProps<TData>) {
  const {
    pagination: { pageIndex, pageSize },
  } = table.getState();

  const currentPage = pageIndex + 1;
  const totalPages = table.getPageCount();

  const canPreviousPage = table.getCanPreviousPage();
  const canNextPage = table.getCanNextPage();

  const pageNumbers = getPageNumbers(currentPage, totalPages);

  return (
    <div
      className={cn(
        'flex items-center justify-between overflow-clip px-2',
        '@max-2xl/content:flex-col-reverse @max-2xl/content:gap-4',
        className,
      )}
      style={{ overflowClipMargin: 1 }}
    >
      <div className="flex w-full items-center justify-between">
        <div className="flex w-25 items-center justify-center text-sm font-medium @2xl/content:hidden">
          Page {currentPage} of {totalPages}
        </div>

        <div className="flex items-center gap-2 @max-2xl/content:flex-row-reverse">
          <Select value={`${pageSize}`} onChange={(value) => table.setPageSize(Number(value))}>
            <SelectTrigger className="h-8 w-17.5">
              <SelectValue />
            </SelectTrigger>

            <SelectContent>
              {PAGE_SIZES.map((size) => (
                <SelectItem key={size} value={`${size}`}>
                  {size}
                </SelectItem>
              ))}
            </SelectContent>
          </Select>

          <p className="hidden text-sm font-medium sm:block">Rows per page</p>
        </div>
      </div>

      <div className="flex items-center sm:space-x-6 lg:space-x-8">
        <div className="flex w-25 items-center justify-center text-sm font-medium @max-3xl/content:hidden">
          Page {currentPage} of {totalPages}
        </div>

        <div className="flex items-center space-x-2">
          <Button
            variant="outline"
            className="size-8 p-0 @max-md/content:hidden"
            isDisabled={!canPreviousPage}
            onClick={() => table.setPageIndex(0)}
          >
            <span className="sr-only">Go to first page</span>
            <ChevronsLeft className="size-4" />
          </Button>

          <Button variant="outline" className="size-8 p-0" isDisabled={!canPreviousPage} onClick={table.previousPage}>
            <span className="sr-only">Go to previous page</span>
            <ChevronLeft className="size-4" />
          </Button>

          {pageNumbers.map((page, index) =>
            page === '...' ? (
              <span key={index} className="px-1 text-sm text-muted-foreground">
                ...
              </span>
            ) : (
              <Button
                key={page}
                variant={currentPage === page ? 'default' : 'outline'}
                className="h-8 min-w-8 px-2"
                onClick={() => table.setPageIndex(page - 1)}
              >
                <span className="sr-only">Go to page {page}</span>
                {page}
              </Button>
            ),
          )}

          <Button variant="outline" className="size-8 p-0" isDisabled={!canNextPage} onClick={table.nextPage}>
            <span className="sr-only">Go to next page</span>
            <ChevronRight className="size-4" />
          </Button>

          <Button
            variant="outline"
            className="size-8 p-0 @max-md/content:hidden"
            isDisabled={!canNextPage}
            onClick={() => table.setPageIndex(totalPages - 1)}
          >
            <span className="sr-only">Go to last page</span>
            <ChevronsRight className="size-4" />
          </Button>
        </div>
      </div>
    </div>
  );
}
