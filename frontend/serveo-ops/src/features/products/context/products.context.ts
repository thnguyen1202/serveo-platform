import type { CrudUIState } from '@/shared/context/crud-context';
import type { BaseDialogType } from '@/lib/base-dialog-type';
import { createContext } from 'react';
import z from 'zod';

const productSchema = z.object({
  id: z.string(),
  name: z.string(),
});

export type Product = z.infer<typeof productSchema>;
export type ProductsDialogType = BaseDialogType;

export const ProductsContext = createContext<CrudUIState<Product, ProductsDialogType> | null>(null);
