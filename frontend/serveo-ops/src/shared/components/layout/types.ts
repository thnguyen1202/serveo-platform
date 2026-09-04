import type { NavGroup } from '@/shared/components/common/nav-group/nav.type';

type User = {
  name: string;
  email: string;
  avatar: string;
};

type Team = {
  name: string;
  logo: React.ComponentType<{ className?: string }> | React.ElementType;
  plan: string;
};

export type SidebarData = {
  user: User;
  teams: Team[];
  navGroups: NavGroup[];
};
