import { DirectionContext } from '@/shared/context/direction-context';
import { useContext } from 'react';

export function useDirection() {
  const context = useContext(DirectionContext);

  if (context === undefined) {
    throw new Error('useDirection must be used within a DirectionProvider');
  }

  return context;
}
