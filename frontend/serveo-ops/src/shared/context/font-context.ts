import { createContext } from 'react';

import { fonts } from '@/shared/config/fonts';

export type Font = (typeof fonts)[number];

type FontContextType = {
  font: Font;
  setFont: (font: Font) => void;
  resetFont: () => void;
};

export const FontContext = createContext<FontContextType | undefined>(undefined);
