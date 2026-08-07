import { useContext } from 'react';
import { TenantContext } from './context';

export function useTenants() {
  const context = useContext(TenantContext);

  if (!context) {
    throw new Error('useTenants must be used within <TenantProvider>.');
  }

  return context;
}
