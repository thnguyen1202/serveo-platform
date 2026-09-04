import { Button } from '@/shared/components/ui/button';
import { cn } from '@/lib/utils';
import { RotateCcw } from 'lucide-react';

export function SectionTitle({
  title,
  showReset = false,
  onReset,
  resetAriaLabel,
  className,
}: {
  title: string;
  showReset?: boolean;
  onReset?: () => void;
  /** Shown on the small per-section reset (RotateCcw) for accessibility and tests. */
  resetAriaLabel?: string;
  className?: string;
}) {
  return (
    <div className={cn('mb-2 flex items-center gap-2 text-sm font-semibold text-muted-foreground', className)}>
      {title}
      {showReset && onReset && (
        <Button
          type="button"
          size="icon"
          variant="secondary"
          className="size-4 rounded-full"
          onClick={onReset}
          aria-label={resetAriaLabel}
        >
          <RotateCcw className="size-3" />
        </Button>
      )}
    </div>
  );
}
