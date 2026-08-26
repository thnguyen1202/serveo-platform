import { cn } from '@/lib/utils';
import { CircleCheck } from 'lucide-react';
import type { SVGProps } from 'react';
import { RadioButton, RadioField, Text } from 'react-aria-components';

export function RadioGroupItem({
  item,
  isTheme = false,
}: {
  item: {
    value: string;
    label: string;
    icon: (props: SVGProps<SVGSVGElement>) => React.ReactElement;
  };
  isTheme?: boolean;
}) {
  return (
    <RadioField
      value={item.value}
      className={cn('group outline-none text-center', 'transition duration-200 ease-in')}
      aria-label={`Select ${item.label.toLowerCase()}`}
      aria-describedby={`${item.value}-description`}
    >
      <RadioButton>
        <div
          className={cn(
            'relative rounded-[6px] ring-[1px] ring-border',
            'group-data-[selected=true]:shadow-2xl group-data-[selected=true]:ring-primary',
            'group-focus-visible:ring-2',
          )}
          role="img"
          aria-hidden="false"
          aria-label={`${item.label} option preview`}
        >
          <CircleCheck
            className={cn(
              'size-6 fill-primary stroke-white',
              'group-not-data-selected:hidden',
              'absolute top-0 right-0 translate-x-1/2 -translate-y-1/2',
            )}
            aria-hidden="true"
          />
          <item.icon
            className={cn(
              !isTheme &&
                'fill-primary stroke-primary group-not-data-selected:fill-muted-foreground group-not-data-selected:stroke-muted-foreground',
            )}
            aria-hidden="true"
          />
        </div>
        <Text slot="description" className="text-xs" id={`${item.value}-description`} aria-live="polite">
          {item.label}
        </Text>
      </RadioButton>
    </RadioField>
  );
}
