import { LayoutContext } from '@/shared/context/layout-context';
import { useContext } from 'react';

export function useLayout() {
  const context = useContext(LayoutContext);
  if (!context) {
    throw new Error('useLayout must be used within a LayoutProvider');
  }
  return context;
}
