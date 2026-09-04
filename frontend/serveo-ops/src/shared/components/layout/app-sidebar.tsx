import { useLayout } from '@/shared/hooks/use-layout';
import { Sidebar, SidebarContent, SidebarFooter, SidebarHeader, SidebarRail } from '@/shared/components/ui/sidebar';
import { sidebarData } from './data/sidebar-data';

import { TeamSwitcher } from './team-switcher';
import { NavGroup } from '../common/nav-group';

export function AppSidebar() {
  const { collapsible, variant } = useLayout();

  return (
    <Sidebar collapsible={collapsible} variant={variant}>
      <SidebarHeader>
        <TeamSwitcher teams={sidebarData.teams as any} />
      </SidebarHeader>
      <SidebarContent className="scrollbar-default">
        {sidebarData.navGroups.map((props) => (
          <NavGroup key={props.title} {...props} isDisabled={true} />
        ))}
      </SidebarContent>
      <SidebarFooter></SidebarFooter>
      <SidebarRail />
    </Sidebar>
  );
}
