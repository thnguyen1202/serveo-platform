import { createFileRoute } from '@tanstack/react-router';
import { MenusPage } from '@/features/menu/pages/menus';

export const Route = createFileRoute('/_authenticated/menu/')({
  component: MenusPage,
});
