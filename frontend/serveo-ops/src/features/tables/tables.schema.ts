import { zodResolver } from '@hookform/resolvers/zod';
import z from 'zod';

// --- table page item ---
const tableSchema = z.object({
  id: z.string(),
  name: z.string(),
  capacity: z.number(),
  status: z.string(),
});

export type Table = z.infer<typeof tableSchema>;

// --- category create ---
export const tableCreateRequestSchema = z.object({
  name: z.string().trim()
    .min(1, 'validation.required')
    .min(2, 'validation.minLength')
    .max(128, 'validation.maxLength'),
    capacity: z.number().min(0, 'validation.minPrice'),
});
export type TableCreateRequest = z.infer<typeof tableCreateRequestSchema>;
export const tableCreateResolver = zodResolver(tableCreateRequestSchema);
