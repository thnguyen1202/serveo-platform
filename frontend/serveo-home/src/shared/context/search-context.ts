import { createContext } from "react";

type SearchContextType = {
  open: boolean;
  openSearch: () => void;
  closeSearch: () => void;
  toggleSearch: () => void;
};

export const SearchContext = createContext<SearchContextType | undefined>(undefined);
