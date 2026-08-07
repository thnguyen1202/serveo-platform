import { useState } from 'react';

import { ChevronsUpDown } from 'lucide-react';

import { DropdownMenu, DropdownMenuGroup, DropdownMenuItem, DropdownMenuTrigger } from '@/components/ui/dropdown-menu';

import { SidebarMenu, SidebarMenuButton, SidebarMenuItem, useSidebar } from '@/components/ui/sidebar';

type Team = {
  name: string;
  logo: React.ComponentType<{
    className?: string;
  }>;
  plan: string;
};

type TeamSwitcherProps = {
  teams: Team[];
  onChange?: (team: Team) => void;
};

export function TeamSwitcher({ teams, onChange }: TeamSwitcherProps) {
  const { isMobile } = useSidebar();

  const [activeTeam, setActiveTeam] = useState(() => teams[0]);

  const hasMultipleTeams = teams.length > 1;

  if (!activeTeam) {
    return null;
  }

  const ActiveLogo = activeTeam.logo;

  const handleChange = (team: Team) => {
    setActiveTeam(team);

    onChange?.(team);
  };

  return (
    <SidebarMenu>
      <SidebarMenuItem>
        <DropdownMenuTrigger>
          <SidebarMenuButton
            size="lg"
            className="
              data-[state=open]:bg-sidebar-accent
              data-[state=open]:text-sidebar-accent-foreground
            "
          >
            <div
              className="
                flex aspect-square size-8
                items-center justify-center
                rounded-lg
                bg-sidebar-primary
                text-sidebar-primary-foreground
              "
            >
              <ActiveLogo className="size-4" />
            </div>

            <div
              className="
                grid flex-1 text-start
                text-sm leading-tight
              "
            >
              <span className="truncate font-semibold">{activeTeam.name}</span>

              <span className="truncate text-xs">{activeTeam.plan}</span>
            </div>

            {hasMultipleTeams && <ChevronsUpDown className="ms-auto" />}
          </SidebarMenuButton>

          {hasMultipleTeams && (
            <DropdownMenu
              placement={isMobile ? 'bottom' : 'right'}
              className="
                w-(--trigger-width)
                min-w-56
                rounded-lg
              "
            >
              <DropdownMenuGroup>
                {teams.map((team) => {
                  const Logo = team.logo;

                  return (
                    <DropdownMenuItem key={team.name} onClick={() => handleChange(team)} className="gap-2 p-2">
                      <div className="flex size-6 items-center justify-center rounded-sm border">
                        <Logo className="size-4 shrink-0" />
                      </div>

                      {team.name}
                    </DropdownMenuItem>
                  );
                })}
              </DropdownMenuGroup>
            </DropdownMenu>
          )}
        </DropdownMenuTrigger>
      </SidebarMenuItem>
    </SidebarMenu>
  );
}
