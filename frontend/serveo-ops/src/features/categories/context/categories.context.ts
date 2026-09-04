import type { CrudUIState } from '@/shared/context/crud-context';
import type { BaseDialogType } from '@/lib/base-dialog-type';
import { createContext } from 'react';
import z from 'zod';

const categorySchema = z.object({
  id: z.string(),
  name: z.string(),
});

export type Category = z.infer<typeof categorySchema>;
export type CategoriesDialogType = BaseDialogType;

export const CategoriesContext = createContext<CrudUIState<Category, CategoriesDialogType> | null>(null);
