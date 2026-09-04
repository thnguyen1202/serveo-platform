import { zodResolver } from '@hookform/resolvers/zod';
import z from 'zod';

const menuSchema = z.object({
  id: z.string(),
  name: z.string(),
});

export type Menu = z.infer<typeof menuSchema>;


// --- category ---
export const categoryCreateRequestSchema = z.object({
  name: z.string().trim()
    .min(1, 'validation.required')
    .min(2, 'validation.minLength')
    .max(128, 'validation.maxLength'),
});
export type CategoryCreateRequest = z.infer<typeof categoryCreateRequestSchema>;
export const categoryCreateResolver = zodResolver(categoryCreateRequestSchema);

// --- product ---
const productCreateSchema = z.object({
  categoryId: z.string(),
  name: z.string({ error: 'validation.required'})
    .trim()
    .min(1, 'validation.required')
    .min(2, 'validation.minLength')
    .max(128, 'validation.maxLength'),
  price: z.number().min(0, 'validation.minPrice'),
  description: z.string().max(128, 'validation.maxLength').optional(),
  imageUrl: z.string().optional()
});

export type ProductCreateFieldValues = z.infer<typeof productCreateSchema>;
export const productCreateResolver = zodResolver(productCreateSchema);