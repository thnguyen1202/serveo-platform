interface Props {
  title: string;
  description?: string;
  action?: React.ReactNode;
}

export function PageHeader({ title, description, action }: Props) {
  return (
    <div className="flex items-center justify-between mb-6">
      <div>
        <h1 className="text-2xl font-semibold">{title}</h1>

        <p className="text-sm text-muted-foreground">{description}</p>
      </div>

      <div>{action}</div>
    </div>
  );
}
