import type { TFunction } from 'i18next';
import { z } from 'zod';

export const loginSchema = z.object({
  // email: z.string().email(),
  email: z.string().trim().min(1, 'validation.required').email(),
  // username: z.string().min(8, {
  //   message: JSON.stringify({
  //     key: 'validation.minLength',
  //     count: 8,
  //   }),
  // }),
  // email: z.string(),
  password: z.string().min(1, 'validation.required'),
});

export type LoginFormValues = z.infer<typeof loginSchema>;

export function createLoginSchema(t: TFunction) {
  return z.object({
    email: z.string().trim().min(1, t('validation.required')).email(),
    password: z.string().min(1, t('validation.required')),
  });
}
