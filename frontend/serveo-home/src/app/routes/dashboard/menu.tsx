import { createFileRoute } from "@tanstack/react-router";

export const Route = createFileRoute("/dashboard/menu")({
  component: RouteComponent,
});

function RouteComponent() {
  return <div>Hello "/dashboard/menu"!</div>;
}
