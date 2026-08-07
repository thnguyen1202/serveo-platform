import { SidebarMenuButton, SidebarMenuItem } from '@/components/ui/sidebar';
import type { NavCollapsible } from './nav.type';
import {
  DropdownMenu,
  DropdownMenuGroup,
  DropdownMenuItem,
  DropdownMenuLabel,
  DropdownMenuSeparator,
  DropdownMenuTrigger,
} from '@/components/ui/dropdown-menu';
import { checkIsActive } from './nav.utils';
import { NavBadge } from './nav-badge';
import { ChevronRight } from 'lucide-react';

export function NavDropdown({ item, href }: { item: NavCollapsible; href: string }) {
  return (
    <SidebarMenuItem>
      <DropdownMenuTrigger>
        <SidebarMenuButton tooltip={item.title} isActive={checkIsActive(href, item)}>
          {item.icon && <item.icon />}
          <span>{item.title}</span>
          {item.badge && <NavBadge>{item.badge}</NavBadge>}
          <ChevronRight className="ms-auto transition-transform duration-200 group-data-[state=open]/collapsible:rotate-90" />
        </SidebarMenuButton>

        <DropdownMenu
          placement="right top"
          className="
                w-(--trigger-width)
                min-w-48
              "
        >
          <DropdownMenuGroup>
            <DropdownMenuLabel className="text-sm">
              {item.title} {item.badge ? `(${item.badge})` : ''}
            </DropdownMenuLabel>
          </DropdownMenuGroup>

          <DropdownMenuSeparator />

          <DropdownMenuGroup>
            {item.items.map((sub) => (
              <DropdownMenuItem
                href={sub.url}
                key={`${sub.title}-${sub.url}`}
                className={`${checkIsActive(href, sub) ? 'bg-secondary' : ''}`}
              >
                {sub.icon && <sub.icon />}
                <span className="max-w-52 text-wrap">{sub.title}</span>
                {sub.badge && <span className="ms-auto text-xs">{sub.badge}</span>}
              </DropdownMenuItem>
            ))}
          </DropdownMenuGroup>
        </DropdownMenu>
      </DropdownMenuTrigger>
    </SidebarMenuItem>
  );
}
