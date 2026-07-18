import { useLocation } from 'react-router-dom';

export default function Breadcrumb() {
  const location = useLocation();

  const paths = location.pathname.split('/').filter(Boolean);

  return (
    <div
      className="
mb-4
text-sm
text-muted-foreground
"
    >
      {paths.map((x, i) => (
        <span key={i}>
          {x}

          {i < paths.length - 1 && ' / '}
        </span>
      ))}
    </div>
  );
}
