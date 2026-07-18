import { Card, CardContent } from '@/components/ui/card';

interface Props {
  title: string;
  value: string | number;
  description?: string;
}

export function KpiCard({ title, value, description }: Props) {
  return (
    <Card>
      <CardContent className="p-6">
        <p
          className="
text-sm
text-muted-foreground
"
        >
          {title}
        </p>

        <p
          className="
text-3xl
font-bold
"
        >
          {value}
        </p>

        <p
          className="
text-xs
text-muted-foreground
"
        >
          {description}
        </p>
      </CardContent>
    </Card>
  );
}
