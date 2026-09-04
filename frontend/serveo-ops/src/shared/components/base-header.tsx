import { ConfigDrawer } from '@/shared/components/common/config-drawer';
import { Header } from '@/shared/components/layout/header';
import { ProfileDropdown } from '@/shared/components/profile-dropdown';
import { Search } from '@/shared/components/search-navigate/search';
import { ThemeSwitch } from '@/shared/components/theme-switch';

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
