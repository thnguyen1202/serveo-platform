import { createContext } from 'react';

export type Theme = 'dark' | 'light' | 'system';

export type ResolvedTheme = Exclude<Theme, 'system'>;

type ThemeContextType = {
  defaultTheme: Theme;
  resolvedTheme: ResolvedTheme;
  theme: Theme;

  setTheme: (theme: Theme) => void;

  resetTheme: () => void;
};

export const ThemeContext = createContext<ThemeContextType | undefined>(undefined);
