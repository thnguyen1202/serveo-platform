import { SidebarMenuButton } from '@/shared/components/ui/sidebar';
import { Link } from '@tanstack/react-router';

type Props = {
  to: string;
  children: React.ReactNode;
  isActive?: boolean;
  tooltip?: string;
};

export function SidebarMenuLink({ to, children, isActive, tooltip }: Props) {
  return (
    <Link to={to}>
      <SidebarMenuButton isActive={isActive} tooltip={tooltip}>
        {children}
      </SidebarMenuButton>
    </Link>
  );
}
