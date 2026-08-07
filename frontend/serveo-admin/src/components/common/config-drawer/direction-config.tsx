import { RadioGroup } from 'react-aria-components';
import { SectionTitle } from './section-title';
import { useDirection } from '@/hooks/use-direction';
import type { SVGProps } from 'react';
import { IconDir } from '@/assets/custom/icon-dir';
import type { Direction } from '@/context/direction-context';
import { RadioGroupItem } from './radio-group-item';

export function DirectionConfig() {
  const { defaultDir, dir, setDir } = useDirection();
  const items = [
    {
      value: 'ltr',
      label: 'Left to Right',
      icon: (props: SVGProps<SVGSVGElement>) => <IconDir dir="ltr" {...props} />,
    },
    {
      value: 'rtl',
      label: 'Right to Left',
      icon: (props: SVGProps<SVGSVGElement>) => <IconDir dir="rtl" {...props} />,
    },
  ];
  return (
    <section key="direction-config">
      <SectionTitle
        title="Direction"
        showReset={defaultDir !== dir}
        onReset={() => setDir(defaultDir)}
        resetAriaLabel="Reset text direction to default"
      />

      <RadioGroup
        value={dir}
        onChange={(value) => setDir(value as Direction)}
        className="grid w-full max-w-md grid-cols-3 gap-4"
        aria-label="Select site direction"
        aria-describedby="direction-description"
      >
        {items.map((item) => (
          <RadioGroupItem key={item.value} item={item} />
        ))}
      </RadioGroup>

      <div id="direction-description" className="sr-only">
        Choose between left-to-right or right-to-left site direction
      </div>
    </section>
  );
}
