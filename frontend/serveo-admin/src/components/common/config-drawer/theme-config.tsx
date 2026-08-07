import { useTheme } from '@/hooks/use-theme';
import { SectionTitle } from './section-title';
import { IconThemeSystem } from '@/assets/custom/icon-theme-system';
import { IconThemeLight } from '@/assets/custom/icon-theme-light';
import { IconThemeDark } from '@/assets/custom/icon-theme-dark';
import { RadioGroup } from 'react-aria-components';
import type { Theme } from '@/context/theme-context';
import { RadioGroupItem } from './radio-group-item';

export function ThemeConfig() {
  const { defaultTheme, theme, setTheme } = useTheme();
  const items = [
    {
      value: 'system',
      label: 'System',
      icon: IconThemeSystem,
    },
    {
      value: 'light',
      label: 'Light',
      icon: IconThemeLight,
    },
    {
      value: 'dark',
      label: 'Dark',
      icon: IconThemeDark,
    },
  ];

  return (
    <section key="theme-config">
      <SectionTitle
        title="Theme"
        showReset={theme !== defaultTheme}
        onReset={() => setTheme(defaultTheme)}
        resetAriaLabel="Reset theme preference to default"
      />

      <RadioGroup
        value={theme}
        onChange={(value) => setTheme(value as Theme)}
        className="grid w-full max-w-md grid-cols-3 gap-4"
      >
        {items.map((item) => (
          <RadioGroupItem key={item.value} item={item} isTheme />
        ))}
      </RadioGroup>

      <div id="theme-description" className="sr-only">
        Choose between system preference, light mode, or dark mode
      </div>
    </section>
  );
}
