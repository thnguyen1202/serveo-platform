import {
  LayoutDashboard,
  Settings,
  UserCog,
  Users,
  Command,
  Building2,
  Factory,
  Store,
  IdCardLanyard,
  NotebookTabs,
  SquareMenu,
  FolderKanban,
  Languages,
  LayoutGrid,
  Logs,
  UserCheck,
  RotateCcwKey,
  Landmark,
  GalleryVerticalEnd,
} from 'lucide-react';

import { type SidebarData } from '../types';

export const sidebarData: SidebarData = {
  user: {
    name: 'satnaing',
    email: 'satnaingdev@gmail.com',
    avatar: '/avatars/shadcn.jpg',
  },
  teams: [
    {
      name: 'Serveo Admin',
      logo: Command,
      plan: 'Super Admin',
    },
  ],
  navGroups: [
    {
      title: 'General',
      items: [
        {
          title: 'Dashboard',
          url: '/',
          icon: LayoutDashboard,
        },
        {
          title: 'Tenanting',
          icon: Landmark,
          items: [
            {
              title: 'Tenants',
              url: '/tenants',
              icon: Building2,
            },
            {
              title: 'Businesses',
              url: '/businesses',
              icon: Factory,
            },
            {
              title: 'Branches',
              url: '/branches',
              icon: Store,
            },
            {
              title: 'Staff',
              url: '/staff',
              icon: IdCardLanyard,
            },
            {
              title: 'Tables',
              url: '/tables',
              icon: LayoutGrid,
            },
          ],
        },
        {
          title: 'Catalog',
          icon: GalleryVerticalEnd,
          items: [
            {
              title: 'Categories',
              url: '/categories',
              icon: NotebookTabs,
            },
            {
              title: 'Menus',
              url: '/menus',
              icon: SquareMenu,
            },
            {
              title: 'Products',
              url: '/products',
              icon: FolderKanban,
            },
          ],
        },
      ],
    },
    {
      title: 'Authorization',
      items: [
        {
          title: 'Settings',
          url: '/settings',
          icon: Settings,
        },
        {
          title: 'Languages',
          url: '/languages',
          icon: Languages,
        },
        {
          title: 'Users',
          url: '/users',
          icon: Users,
        },
        {
          title: 'Roles',
          url: '/roles',
          icon: UserCog,
        },
        {
          title: 'User Sessions',
          url: '/user-sessions',
          icon: UserCheck,
        },
        {
          title: 'Api Keys',
          url: '/api-keys',
          icon: RotateCcwKey,
        },
        {
          title: 'Audit Log',
          url: '/audit-log',
          icon: Logs,
        },
      ],
    },
  ],
};
