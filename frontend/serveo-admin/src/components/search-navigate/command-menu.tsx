import { useNavigate } from '@tanstack/react-router';
import { ArrowRight, ChevronRight, Laptop, Moon, Sun } from 'lucide-react';

import {
  Command,
  CommandDialog,
  CommandEmpty,
  CommandGroup,
  CommandInput,
  CommandItem,
  CommandList,
  CommandSeparator,
} from '@/components/ui/command';

import { sidebarData } from '@/components/layout/data/sidebar-data';
import { useTheme } from '@/hooks/use-theme';
import { useSearch } from '@/hooks/use-search';

const themes = [
  { value: 'light', label: 'Light', icon: Sun },
  { value: 'dark', label: 'Dark', icon: Moon },
  { value: 'system', label: 'System', icon: Laptop },
] as const;

export function CommandMenu() {
  const navigate = useNavigate();
  const { setTheme } = useTheme();
  const { open, openSearch, toggleSearch } = useSearch();

  const runCommand = (callback: () => void) => {
    openSearch();
    callback();
  };

  return (
    <CommandDialog open={open} onOpenChange={toggleSearch}>
      <Command>
        <CommandInput placeholder="Type a command or search..." />
        <CommandList renderEmptyState={() => <CommandEmpty>No results found.</CommandEmpty>}>
          {sidebarData.navGroups.map((group) => (
            <CommandGroup key={group.title} heading={group.title}>
              {group.items.flatMap((item) => {
                if (item.url) {
                  return (
                    <CommandItem
                      key={item.url}
                      textValue={item.title}
                      onAction={() => runCommand(() => navigate({ to: item.url! }))}
                    >
                      <ArrowRight className="size-3 text-muted-foreground/80" />
                      {item.title}
                    </CommandItem>
                  );
                }

                return item.items?.map((subItem) => (
                  <CommandItem
                    key={subItem.url}
                    textValue={`${item.title} ${subItem.title}`}
                    onAction={() => runCommand(() => navigate({ to: subItem.url }))}
                  >
                    <ArrowRight className="size-3 text-muted-foreground/80" />
                    {item.title}
                    <ChevronRight className="size-3" />
                    {subItem.title}
                  </CommandItem>
                ));
              })}
            </CommandGroup>
          ))}
          <CommandSeparator />
          <CommandGroup heading="Theme">
            {themes.map(({ value, label, icon: Icon }) => (
              <CommandItem key={value} onAction={() => runCommand(() => setTheme(value))}>
                <Icon />
                <span>{label}</span>
              </CommandItem>
            ))}
          </CommandGroup>
        </CommandList>
      </Command>
    </CommandDialog>
  );
}
