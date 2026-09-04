import { useCallback, useEffect, useMemo, useState } from 'react';

import { CommandMenu } from '@/shared/components/search-navigate/command-menu';
import { SearchContext } from '@/shared/context/search-context';

type SearchProviderProps = {
  children: React.ReactNode;
};

export function SearchProvider({ children }: SearchProviderProps) {
  const [open, setOpen] = useState(false);

  const openSearch = useCallback(() => {
    setOpen(true);
  }, []);

  const closeSearch = useCallback(() => {
    setOpen(false);
  }, []);

  const toggleSearch = useCallback(() => {
    setOpen((value) => !value);
  }, []);

  useEffect(() => {
    const handleKeyDown = (event: KeyboardEvent) => {
      const target = event.target as HTMLElement;

      const isInput = target.tagName === 'INPUT' || target.tagName === 'TEXTAREA' || target.isContentEditable;

      if (isInput) {
        return;
      }

      if (event.key.toLowerCase() === 'k' && (event.metaKey || event.ctrlKey)) {
        event.preventDefault();

        toggleSearch();
      }
    };

    document.addEventListener('keydown', handleKeyDown);

    return () => {
      document.removeEventListener('keydown', handleKeyDown);
    };
  }, [toggleSearch]);

  const value = useMemo(
    () => ({
      open,
      openSearch,
      closeSearch,
      toggleSearch,
    }),
    [open, openSearch, closeSearch, toggleSearch],
  );

  return (
    <SearchContext.Provider value={value}>
      {children}

      <CommandMenu />
    </SearchContext.Provider>
  );
}
