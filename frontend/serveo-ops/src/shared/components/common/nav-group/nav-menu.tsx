import { Collapsible, CollapsibleContent } from '@/shared/components/ui/collapsible';
import {
  SidebarMenuButton,
  SidebarMenuItem,
  SidebarMenuSub,
  SidebarMenuSubButton,
  SidebarMenuSubItem,
} from '@/shared/components/ui/sidebar';
import { ChevronRight } from 'lucide-react';
import { NavBadge } from './nav-badge';
import { checkIsActive } from './nav.utils';
import type { NavCollapsible } from './nav.type';

export function NavMenu({ item, href }: { item: NavCollapsible; href: string }) {
  return (
    <Collapsible defaultExpanded={checkIsActive(href, item, true)} className="group/menu w-full">
      <SidebarMenuItem className="mb-1">
        <SidebarMenuButton slot="trigger" tooltip={item.title}>
          {item.icon && <item.icon />}
          <span>{item.title}</span>
          {item.badge && <NavBadge>{item.badge}</NavBadge>}
          <ChevronRight className="ms-auto transition-transform duration-200 group-data-expanded/menu:rotate-90 rtl:rotate-180" />
        </SidebarMenuButton>
        <CollapsibleContent className="CollapsibleContent">
          <SidebarMenuSub>
            {item.items.map((subItem) => (
              <SidebarMenuSubItem key={subItem.title}>
                <SidebarMenuSubButton
                  href={checkIsActive(href, subItem) ? undefined : subItem.url}
                  isActive={checkIsActive(href, subItem)}
                >
                  {subItem.icon && <subItem.icon />}
                  <span>{subItem.title}</span>
                  {subItem.badge && <NavBadge>{subItem.badge}</NavBadge>}
                </SidebarMenuSubButton>
              </SidebarMenuSubItem>
            ))}
          </SidebarMenuSub>
        </CollapsibleContent>
      </SidebarMenuItem>
    </Collapsible>
  );
}
