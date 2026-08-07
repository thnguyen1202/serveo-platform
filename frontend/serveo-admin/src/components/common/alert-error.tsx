import { Alert, AlertDescription, AlertTitle } from '@/components/ui/alert';
import { AlertCircleIcon } from 'lucide-react';

export function AlertError({ title, message }: { title: string; message?: string }) {
  return (
    <Alert variant="destructive" className="max-w-md border-none p-0">
      <AlertCircleIcon />
      <AlertTitle>{title}</AlertTitle>
      <AlertDescription className="text-[0.8rem]">{message}</AlertDescription>
    </Alert>
  );
}
