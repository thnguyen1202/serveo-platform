import {
  LayoutDashboard,
  Settings,
  Command,
  IdCardLanyard,
  SquareMenu,
  BookA,
  ChefHat,
  Dices,
  ArrowBigUp,
  Summary,
  ListTodo,
  SquareStack,
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
      title: 'Operation',
      items: [
        {
          title: 'Dashboard',
          url: '/',
          icon: LayoutDashboard,
        },
        {
          title: 'Orders',
          url: '/orders',
          icon: BookA,
        },
        {
          title: 'Kitchen',
          url: '/kitchen',
          icon: ChefHat,
        },
        {
          title: 'Tables',
          url: '/tables',
          icon: Dices,
        },
        {
          title: 'Menu',
          url: '/menu',
          icon: SquareMenu,
        },
      ],
    },
    {
      title: 'Management',
      items: [
        {
          title: 'Menu',
          icon: SquareMenu,
          items: [
            {
              title: 'Menus',
              url: '/menu',
              icon: SquareMenu,
            },
            {
              title: 'Items',
              url: '/menu/items',
              icon: ListTodo,
            },
            {
              title: 'Categories',
              url: '/menu/categories',
              icon: SquareStack,
            },
          ],
        },
        {
          title: 'Staff',
          url: '/staff',
          icon: IdCardLanyard,
        },
        {
          title: 'Shifts',
          url: '/shifts',
          icon: ArrowBigUp,
        },
        {
          title: 'Reports',
          url: '/reports',
          icon: Summary,
        },
      ],
    },
    {
      title: 'Configuration',
      items: [
        {
          title: 'Settings',
          url: '/settings',
          icon: Settings,
        },
      ],
    },
  ],
};
