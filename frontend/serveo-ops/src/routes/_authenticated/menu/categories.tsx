import { createFileRoute } from '@tanstack/react-router';
import { CategoriesPage } from '@/features/menu/pages/categories';

export const Route = createFileRoute('/_authenticated/menu/categories')({
  component: CategoriesPage,
});
