import { useCallback, useEffect } from 'react';

import { Check, Moon, Sun } from 'lucide-react';

import { cn } from '@/lib/utils';

import { Button } from './ui/button';
import { Popover } from './ui/popover';
import { Menu, MenuItem, MenuTrigger } from 'react-aria-components';
import { useTheme } from '@/hooks/use-theme';

type Theme = 'light' | 'dark' | 'system';

export function ThemeSwitch() {
  const { theme, setTheme } = useTheme();

  useEffect(() => {
    const themeColor = theme === 'dark' ? '#020817' : '#ffffff';

    const metaThemeColor = document.querySelector("meta[name='theme-color']");

    metaThemeColor?.setAttribute('content', themeColor);
  }, [theme]);

  const handleThemeChange = useCallback(
    (value: Theme) => {
      setTheme(value);
    },
    [setTheme],
  );

  return (
    <MenuTrigger>
      <Button
        aria-label="Toggle theme"
        className={cn('inline-flex', 'size-9', 'items-center', 'justify-center', 'rounded-full', 'hover:bg-accent')}
      >
        <Sun
          className={cn(
            'size-[1.2rem]',
            'transition-all',
            theme === 'dark' ? 'rotate-90 scale-0' : 'rotate-0 scale-100',
          )}
        />

        <Moon
          className={cn(
            'absolute',
            'size-[1.2rem]',
            'transition-all',
            theme === 'dark' ? 'rotate-0 scale-100' : 'rotate-90 scale-0',
          )}
        />
      </Button>

      <Popover
        placement="bottom end"
        offset={8}
        className={cn('z-50', 'min-w-32', 'rounded-md', 'border', 'bg-popover', 'p-1', 'shadow-md')}
      >
        <Menu
          aria-label="Theme"
          selectionMode="single"
          selectedKeys={[theme]}
          onSelectionChange={(keys) => {
            const value = Array.from(keys)[0];

            if (value === 'light' || value === 'dark' || value === 'system') {
              handleThemeChange(value);
            }
          }}
        >
          <MenuItem id="light" className={menuItemClass}>
            Light
            <CheckIcon visible={theme === 'light'} />
          </MenuItem>

          <MenuItem id="dark" className={menuItemClass}>
            Dark
            <CheckIcon visible={theme === 'dark'} />
          </MenuItem>

          <MenuItem id="system" className={menuItemClass}>
            System
            <CheckIcon visible={theme === 'system'} />
          </MenuItem>
        </Menu>
      </Popover>
    </MenuTrigger>
  );
}

function CheckIcon({ visible }: { visible: boolean }) {
  return <Check size={14} className={cn('ms-auto', !visible && 'invisible')} />;
}

const menuItemClass = cn(
  'flex',
  'cursor-default',
  'items-center',
  'rounded-sm',
  'px-2',
  'py-1.5',
  'text-sm',
  'outline-none',
  'data-[focused]:bg-accent',
  'data-[focused]:text-accent-foreground',
);
