import { LoaderCircle } from 'lucide-react';

export function LoadingScreen() {
  return (
    <div className="h-svh">
      <div className="m-auto flex h-full w-full flex-col items-center justify-center gap-2">
        <LoaderCircle size={72} className="animate-spin" />
        <h1 className="text-muted-foreground animate-caret-blink shimmer">Loading...</h1>
      </div>
    </div>
  );
}
