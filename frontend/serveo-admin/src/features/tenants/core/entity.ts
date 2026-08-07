import z from 'zod';

const entitySchema = z.object({
  id: z.string(),
  name: z.string(),
  expiredTime: z.string(),
});

export type Tenant = z.infer<typeof entitySchema>;
