import { createFileRoute } from '@tanstack/react-router';
import { Menu } from '@/features/menu/pages';

export const Route = createFileRoute('/_authenticated/menu')({
  component: Menu,
});
