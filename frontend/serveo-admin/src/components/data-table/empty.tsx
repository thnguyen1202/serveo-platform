import { Inbox } from 'lucide-react';

import { cn } from '@/lib/utils';

interface DataTableEmptyProps {
  title?: string;
  description?: string;
  action?: React.ReactNode;
  className?: string;
}

export function DataTableEmpty({
  title = 'No data found',
  description = 'There are no records to display.',
  action,
  className,
}: DataTableEmptyProps) {
  return (
    <div className={cn('flex min-h-60 flex-col items-center justify-center gap-3', className)}>
      <div className="rounded-full bg-muted p-3">
        <Inbox className="size-6 text-muted-foreground" />
      </div>

      <div className="text-center">
        <h3 className="font-medium">{title}</h3>

        <p className="text-sm text-muted-foreground">{description}</p>
      </div>

      {action}
    </div>
  );
}
