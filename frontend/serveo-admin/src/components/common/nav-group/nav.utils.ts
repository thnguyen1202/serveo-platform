import type { NavItem } from './nav.type';

export function checkIsActive(href: string, item: NavItem, mainNav = false): boolean {
  const pathname = href.split('?')[0];

  return (
    pathname === item.url ||
    item.items?.some((i) => i.url === pathname) ||
    (mainNav && pathname.split('/')[1] === item?.url?.split('/')[1])
  );
}
