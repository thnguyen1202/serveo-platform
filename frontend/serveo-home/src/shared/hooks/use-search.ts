import { useContext } from "react";

import { SearchContext } from "@/shared/context/search-context";

export function useSearch() {
  const context = useContext(SearchContext);

  if (!context) {
    throw new Error("useSearch must be used within SearchProvider");
  }

  return context;
}
