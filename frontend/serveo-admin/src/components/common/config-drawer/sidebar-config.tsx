import { RadioGroup } from 'react-aria-components';
import { SectionTitle } from './section-title';
import { useLayout } from '@/hooks/use-layout';
import { IconSidebarInset } from '@/assets/custom/icon-sidebar-inset';
import { IconSidebarFloating } from '@/assets/custom/icon-sidebar-floating';
import { IconSidebarSidebar } from '@/assets/custom/icon-sidebar-sidebar';
import type { Variant } from '@/context/layout-context';
import { RadioGroupItem } from './radio-group-item';

export function SidebarConfig() {
  const { defaultVariant, variant, setVariant } = useLayout();
  const items = [
    {
      value: 'inset',
      label: 'Inset',
      icon: IconSidebarInset,
    },
    {
      value: 'floating',
      label: 'Floating',
      icon: IconSidebarFloating,
    },
    {
      value: 'sidebar',
      label: 'Sidebar',
      icon: IconSidebarSidebar,
    },
  ];

  return (
    <section key="sidebar-config" className="max-md:hidden">
      <SectionTitle
        title="Sidebar"
        showReset={defaultVariant !== variant}
        onReset={() => setVariant(defaultVariant)}
        resetAriaLabel="Reset sidebar style to default"
      />
      <RadioGroup
        value={variant}
        onChange={(value) => setVariant(value as Variant)}
        className="grid w-full max-w-md grid-cols-3 gap-4"
        aria-label="Select sidebar style"
        aria-describedby="sidebar-description"
      >
        {items.map((item) => (
          <RadioGroupItem key={item.value} item={item} />
        ))}
      </RadioGroup>
      <div id="sidebar-description" className="sr-only">
        Choose between inset, floating, or standard sidebar layout
      </div>
    </section>
  );
}
