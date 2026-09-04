import { useCallback, useEffect, useMemo, useState, type ReactNode } from "react";
import { I18nProvider } from "react-aria";

import { getCookie, removeCookie, setCookie } from "@/lib/cookies";
import { DirectionContext } from "@/shared/context/direction-context";
import i18n from "@/i18n";

export type Direction = "ltr" | "rtl";

const DEFAULT_DIRECTION: Direction = "ltr";
const DIRECTION_COOKIE_NAME = "dir";
const DIRECTION_COOKIE_MAX_AGE = 60 * 60 * 24 * 365;

function getInitialDirection(): Direction {
  const value = getCookie(DIRECTION_COOKIE_NAME);

  return value === "ltr" || value === "rtl" ? value : DEFAULT_DIRECTION;
}

type DirectionProviderProps = {
  children: ReactNode;
};

export function DirectionProvider({ children }: DirectionProviderProps) {
  const [dir, setDirState] = useState<Direction>(getInitialDirection);

  useEffect(() => {
    document.documentElement.dir = dir;
  }, [dir]);

  const setDir = useCallback((newDir: Direction) => {
    setDirState(newDir);

    setCookie(DIRECTION_COOKIE_NAME, newDir, DIRECTION_COOKIE_MAX_AGE);
  }, []);

  const resetDir = useCallback(() => {
    setDirState(DEFAULT_DIRECTION);
    removeCookie(DIRECTION_COOKIE_NAME);
  }, []);

  const value = useMemo(
    () => ({
      defaultDir: DEFAULT_DIRECTION,
      dir,
      setDir,
      resetDir,
    }),
    [dir, setDir, resetDir],
  );

  return (
    <DirectionContext.Provider value={value}>
      <I18nProvider locale={i18n.dir(i18n.language)}>{children}</I18nProvider>
    </DirectionContext.Provider>
  );
}
