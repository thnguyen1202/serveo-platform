import { Dialog, DialogDescription, DialogHeader, DialogTitle, DialogTrigger } from '@/components/ui/dialog';
import { MenusProvider } from '../../core/provider';
import { MenuManager } from './menu-manager';
import { Button } from '@/components/ui/button';
import { MenusDialogs } from './dialogs';

export function MenusPage() {
  return (
    <>
      <MenusProvider>
        <MenuManager />
        <MenusDialogs />
      </MenusProvider>
    </>
  );
}
