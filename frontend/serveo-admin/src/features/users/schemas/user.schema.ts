import { z } from 'zod';

export const userSchema = z.object({
  username: z.string().min(3),
  email: z.string().email(),
  displayName: z.string().min(2),
  password: z.string().min(8),
});

export type UserFormValues = z.infer<typeof userSchema>;
