import z from 'zod';

const entitySchema = z.object({
  id: z.string(),
  name: z.string(),
});

export type Menu = z.infer<typeof entitySchema>;
