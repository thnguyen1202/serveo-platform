import { useCallback, useEffect, useMemo, useState } from 'react';

import { getCookie, removeCookie, setCookie } from '@/lib/cookies';
import { ThemeContext, type Theme, type ResolvedTheme } from '../context/theme-context';

const DEFAULT_THEME: Theme = 'system';

const THEME_COOKIE_NAME = 'vite-ui-theme';

const THEME_COOKIE_MAX_AGE = 60 * 60 * 24 * 365;

type ThemeProviderProps = {
  children: React.ReactNode;
  defaultTheme?: Theme;
  storageKey?: string;
};

export function ThemeProvider({
  children,
  defaultTheme = DEFAULT_THEME,
  storageKey = THEME_COOKIE_NAME,
}: ThemeProviderProps) {
  const [theme, setThemeState] = useState<Theme>(() => {
    const savedTheme = getCookie(storageKey);

    return savedTheme === 'dark' || savedTheme === 'light' || savedTheme === 'system' ? savedTheme : defaultTheme;
  });

  const getSystemTheme = (): ResolvedTheme => {
    if (typeof window === 'undefined') {
      return 'light';
    }

    return window.matchMedia('(prefers-color-scheme: dark)').matches ? 'dark' : 'light';
  };

  const resolvedTheme: ResolvedTheme = theme === 'system' ? getSystemTheme() : theme;

  useEffect(() => {
    if (typeof window === 'undefined') {
      return;
    }

    const root = document.documentElement;

    const applyTheme = (value: ResolvedTheme) => {
      root.classList.remove('light', 'dark');

      root.classList.add(value);
    };

    applyTheme(resolvedTheme);

    const media = window.matchMedia('(prefers-color-scheme: dark)');

    const listener = () => {
      if (theme === 'system') {
        applyTheme(getSystemTheme());
      }
    };

    media.addEventListener('change', listener);

    return () => {
      media.removeEventListener('change', listener);
    };
  }, [theme, resolvedTheme]);

  const setTheme = useCallback(
    (value: Theme) => {
      setCookie(storageKey, value, THEME_COOKIE_MAX_AGE);

      setThemeState(value);
    },
    [storageKey],
  );

  const resetTheme = useCallback(() => {
    removeCookie(storageKey);

    setThemeState(DEFAULT_THEME);
  }, [storageKey]);

  const contextValue = useMemo(
    () => ({
      defaultTheme,
      resolvedTheme,
      theme,
      setTheme,
      resetTheme,
    }),
    [defaultTheme, resolvedTheme, theme, setTheme, resetTheme],
  );

  return <ThemeContext.Provider value={contextValue}>{children}</ThemeContext.Provider>;
}
