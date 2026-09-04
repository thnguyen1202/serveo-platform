import { cn } from "@/lib/utils";
import { useNavigate, useRouter } from "@tanstack/react-router";
import { Button } from "@/shared/components/ui/button";

type ErrorPageProps = React.ComponentProps<"div"> & {
  code?: number;
  title: string;
  description: React.ReactNode;
  minimal?: boolean;
};

export function ErrorPage({
  code,
  title,
  description,
  minimal = false,
  className,
  ...props
}: ErrorPageProps) {
  const navigate = useNavigate();
  const { history } = useRouter();
  const handleGoBack = () => {
    if (history.canGoBack?.()) {
      history.go(-1);
      return;
    }

    navigate({ to: "/" });
  };

  return (
    <div className={cn("h-svh w-full", className)} {...props}>
      <div className="m-auto flex h-full w-full flex-col items-center justify-center gap-2">
        {!minimal && code && <h1 className="text-[7rem] leading-tight font-bold">{code}</h1>}

        <span className="font-medium">{title}</span>

        <p className="text-center text-muted-foreground">{description}</p>

        {!minimal && (
          <div className="mt-6 flex gap-4">
            <Button variant="outline" onClick={handleGoBack}>
              Go Back
            </Button>

            <Button onClick={() => navigate({ to: "/" })}>Back to Home</Button>
          </div>
        )}
      </div>
    </div>
  );
}
