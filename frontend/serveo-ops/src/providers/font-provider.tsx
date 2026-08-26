import { useCallback, useEffect, useMemo, useState } from 'react';

import { fonts } from '@/core/config/fonts';
import { getCookie, removeCookie, setCookie } from '@/lib/cookies';
import { FontContext, type Font } from '../context/font-context';

const FONT_COOKIE_NAME = 'font';
const FONT_COOKIE_MAX_AGE = 60 * 60 * 24 * 365;

export function FontProvider({ children }: { children: React.ReactNode }) {
  const [font, setFontState] = useState<Font>(() => {
    const savedFont = getCookie(FONT_COOKIE_NAME);

    return fonts.includes(savedFont as Font) ? (savedFont as Font) : fonts[0];
  });

  useEffect(() => {
    if (typeof document === 'undefined') return;

    const root = document.documentElement;

    [...root.classList]
      .filter((cls) => cls.startsWith('font-'))
      .forEach((cls) => {
        root.classList.remove(cls);
      });

    root.classList.add(`font-${font}`);
  }, [font]);

  const setFont = useCallback((font: Font) => {
    setCookie(FONT_COOKIE_NAME, font, FONT_COOKIE_MAX_AGE);

    setFontState(font);
  }, []);

  const resetFont = useCallback(() => {
    removeCookie(FONT_COOKIE_NAME);

    setFontState(fonts[0]);
  }, []);

  const value = useMemo(
    () => ({
      font,
      setFont,
      resetFont,
    }),
    [font, setFont, resetFont],
  );

  return <FontContext.Provider value={value}>{children}</FontContext.Provider>;
}
