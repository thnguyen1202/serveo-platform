import { Building2, LayoutDashboard, Settings, Shield, Users } from 'lucide-react';

export interface MenuItem {
  title: string;

  path: string;

  icon: unknown;

  permission?: string;
}

export const menuItems: MenuItem[] = [
  {
    title: 'Dashboard',
    path: '/admin',
    icon: LayoutDashboard,
  },

  {
    title: 'Users',
    path: '/admin/users',
    icon: Users,
    permission: 'users.view',
  },

  {
    title: 'Roles',
    path: '/admin/roles',
    icon: Shield,
    permission: 'roles.view',
  },

  {
    title: 'Tenants',
    path: '/admin/tenants',
    icon: Building2,
    permission: 'tenant.view',
  },

  {
    title: 'Settings',
    path: '/admin/settings',
    icon: Settings,
  },
];
