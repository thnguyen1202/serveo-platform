import { ConfigDrawer } from '@/components/common/config-drawer';
import { Header } from '@/components/layout/header';
import { ProfileDropdown } from '@/components/profile-dropdown';
import { Search } from '@/components/search-navigate/search';
import { ThemeSwitch } from '@/components/theme-switch';

export function BaseHeader() {
  return (
    <Header fixed>
      <Search className="me-auto" />
      <ThemeSwitch />
      <ConfigDrawer />
      <ProfileDropdown />
    </Header>
  );
}
