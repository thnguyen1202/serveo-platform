import { Skeleton } from '@/components/ui/skeleton';

interface DataTableSkeletonProps {
  columns?: number;
  rows?: number;
}

export function DataTableSkeleton({ columns = 5, rows = 10 }: DataTableSkeletonProps) {
  return (
    <div className="w-full overflow-hidden rounded-md border">
      <table className="w-full">
        <thead>
          <tr className="border-b">
            {Array.from({ length: columns }).map((_, index) => (
              <th key={index} className="h-10 px-4 text-left">
                <Skeleton className="h-4 w-20" />
              </th>
            ))}
          </tr>
        </thead>

        <tbody>
          {Array.from({ length: rows }).map((_, rowIndex) => (
            <tr key={rowIndex} className="border-b">
              {Array.from({ length: columns }).map((_, columnIndex) => (
                <td key={columnIndex} className="h-12 px-4">
                  <Skeleton className={columnIndex === 0 ? 'h-4 w-32' : 'h-4 w-24'} />
                </td>
              ))}
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}
