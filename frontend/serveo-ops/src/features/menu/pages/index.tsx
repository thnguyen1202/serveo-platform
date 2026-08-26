import { Outlet } from '@tanstack/react-router';
import { Palette, Wrench, UserCog } from 'lucide-react';
import { Separator } from '@/components/ui/separator';
import { Main } from '@/components/layout/main';
import { BaseHeader } from '@/components/base-header';
import { Tabs, TabsList, TabsTrigger } from '@/components/ui/tabs';
import { useTranslation } from 'react-i18next';

// const sidebarNavItems = [
//   {
//     title: 'Menus',
//     href: '/menu',
//     icon: <UserCog size={18} />,
//   },
//   {
//     title: 'Items',
//     href: '/menu/items',
//     icon: <Wrench size={18} />,
//   },
//   {
//     title: 'Categories',
//     href: '/menu/categories',
//     icon: <Palette size={18} />,
//   },
// ];

export function Menu() {
  const { t } = useTranslation('common');
  return (
    <>
      {/* ===== Top Heading ===== */}
      <BaseHeader />

      <Main fixed>
        <div className="space-y-0.5">
          <h1 className="text-2xl font-bold tracking-tight">Menu</h1>
          <p className="text-muted-foreground">{t('menu.main.description')}</p>
        </div>

        <Tabs defaultSelectedKey="overview">
          <TabsList>
            <TabsTrigger id="overview" href="/menu">
              <UserCog size={18} /> Menus
            </TabsTrigger>
            <TabsTrigger id="analytics" href="/menu/items">
              <Wrench size={18} /> Items
            </TabsTrigger>
            <TabsTrigger id="reports" href="/menu/categories">
              {' '}
              <Palette size={18} />
              Categories
            </TabsTrigger>
          </TabsList>
        </Tabs>
        <Separator className="my-4 lg:my-6" />
        <div className="flex flex-1 flex-col space-y-2 overflow-hidden md:space-y-2 lg:flex-row lg:space-y-0 lg:space-x-12">
          <div className="w-full overflow-y-hidden p-1">
            <Outlet />
          </div>
        </div>
      </Main>
    </>
  );
}
