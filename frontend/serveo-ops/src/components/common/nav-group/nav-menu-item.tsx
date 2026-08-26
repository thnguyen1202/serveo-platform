import { SidebarMenuButton, SidebarMenuItem } from '@/components/ui/sidebar';
import { checkIsActive } from './nav.utils';
import { NavBadge } from './nav-badge';
import type { NavLink } from './nav.type';

export function NavMenuItem({ item, href }: { item: NavLink; href: string }) {
  const isActive = checkIsActive(href, item);
  const url = isActive ? '' : item.url;
  return (
    <SidebarMenuItem className="mb-1">
      <SidebarMenuButton href={url} isActive={isActive} tooltip={item.title}>
        {item.icon && <item.icon />}
        <span>{item.title}</span>
        {item.badge && <NavBadge>{item.badge}</NavBadge>}
      </SidebarMenuButton>
    </SidebarMenuItem>
  );
}
