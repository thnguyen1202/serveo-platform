import { createFileRoute } from '@tanstack/react-router';
import { ProductsPage } from '@/features/menu/pages/products';

export const Route = createFileRoute('/_authenticated/menu/items')({
  component: ProductsPage,
});
