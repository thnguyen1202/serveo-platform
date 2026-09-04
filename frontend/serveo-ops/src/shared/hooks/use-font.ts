import { FontContext } from '@/shared/context/font-context';
import { useContext } from 'react';

export function useFont() {
  const context = useContext(FontContext);

  if (!context) {
    throw new Error('useFont must be used within FontProvider');
  }

  return context;
}
