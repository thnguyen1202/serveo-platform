import {
  SidebarGroup,
  SidebarGroupContent,
  SidebarGroupLabel,
  SidebarMenu,
  useSidebar,
} from '@/shared/components/ui/sidebar';
import { useLocation } from '@tanstack/react-router';
import { type NavGroup as NavGroupProps } from './nav.type';
import { Collapsible, CollapsibleContent, CollapsibleTrigger } from '@/shared/components/ui/collapsible';
import { ChevronRight } from 'lucide-react';
import { NavMenuItem } from './nav-menu-item';
import { NavMenu } from './nav-menu';
import { NavDropdown } from './nav-dropdown';

export function NavGroup({ title, items, isDisabled = false }: NavGroupProps) {
  const href = useLocation({ select: (location) => location.href });
  const { state, isMobile } = useSidebar();

  return (
    <Collapsible defaultExpanded isDisabled={isDisabled} className="group/sidebar">
      <SidebarGroup>
        <SidebarGroupLabel elementType={CollapsibleTrigger}>
          {title}
          {!isDisabled && (
            <ChevronRight className="ml-auto transition-transform group-data-expanded/sidebar:rotate-90 rtl:rotate-180" />
          )}
        </SidebarGroupLabel>
        <CollapsibleContent>
          <SidebarGroupContent>
            <SidebarMenu>
              {items.map((item) => {
                const key = `${item.title}-${item.url}`;

                if (!item.items) return <NavMenuItem key={key} item={item} href={href} />;

                if (state === 'collapsed' && !isMobile) return <NavDropdown key={key} item={item} href={href} />;

                return <NavMenu key={key} item={item} href={href} />;
              })}
            </SidebarMenu>
          </SidebarGroupContent>
        </CollapsibleContent>
      </SidebarGroup>
    </Collapsible>
  );
}
