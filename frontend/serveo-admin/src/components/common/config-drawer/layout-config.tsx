import { SectionTitle } from './section-title';
import { useSidebar } from '@/components/ui/sidebar';
import { useLayout } from '@/hooks/use-layout';
import { RadioGroup } from 'react-aria-components';
import { RadioGroupItem } from './radio-group-item';
import type { Collapsible } from '@/context/layout-context';
import { IconLayoutFull } from '@/assets/custom/icon-layout-full';
import { IconLayoutCompact } from '@/assets/custom/icon-layout-compact';
import { IconLayoutDefault } from '@/assets/custom/icon-layout-default';

export function LayoutConfig() {
  const { open, setOpen } = useSidebar();
  const { defaultCollapsible, collapsible, setCollapsible } = useLayout();
  const radioState = open ? 'default' : collapsible;
  const items = [
    {
      value: 'default',
      label: 'Default',
      icon: IconLayoutDefault,
    },
    {
      value: 'icon',
      label: 'Compact',
      icon: IconLayoutCompact,
    },
    {
      value: 'offcanvas',
      label: 'Full layout',
      icon: IconLayoutFull,
    },
  ];

  return (
    <section key="layout-config" className="max-md:hidden">
      <SectionTitle
        title="Layout"
        showReset={radioState !== 'default'}
        onReset={() => {
          setOpen(true);
          setCollapsible(defaultCollapsible);
        }}
        resetAriaLabel="Reset layout options to default"
      />

      <RadioGroup
        value={radioState}
        onChange={(v) => {
          if (v === 'default') {
            setOpen(true);
            return;
          }
          setOpen(false);
          setCollapsible(v as Collapsible);
        }}
        className="grid w-full max-w-md grid-cols-3 gap-4"
        aria-label="Select layout style"
        aria-describedby="layout-description"
      >
        {items.map((item) => (
          <RadioGroupItem key={item.value} item={item} />
        ))}
      </RadioGroup>

      <div id="layout-description" className="sr-only">
        Choose between default expanded, compact icon-only, or full layout mode
      </div>
    </section>
  );
}
