import { createContext } from 'react';

export type Direction = 'ltr' | 'rtl';

type DirectionContextType = {
  defaultDir: Direction;
  dir: Direction;
  setDir: (dir: Direction) => void;
  resetDir: () => void;
};

export const DirectionContext = createContext<DirectionContextType | undefined>(undefined);
