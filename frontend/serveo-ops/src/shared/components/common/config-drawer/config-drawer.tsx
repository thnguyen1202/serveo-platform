import { Button } from '@/shared/components/ui/button';
import {
  Sheet,
  SheetDescription,
  SheetFooter,
  SheetHeader,
  SheetTitle,
  SheetTrigger,
} from '@/shared/components/ui/sheet';
import { useSidebar } from '@/shared/components/ui/sidebar';
import { useDirection } from '@/shared/hooks/use-direction';
import { useLayout } from '@/shared/hooks/use-layout';
import { useTheme } from '@/shared/hooks/use-theme';
import { Settings } from 'lucide-react';
import { ThemeConfig } from './theme-config';
import { SidebarConfig } from './sidebar-config';
import { LayoutConfig } from './layout-config';
import { DirectionConfig } from './direction-config';

export function ConfigDrawer() {
  const { setOpen } = useSidebar();
  const { resetDir } = useDirection();
  const { resetTheme } = useTheme();
  const { resetLayout } = useLayout();

  const handleReset = () => {
    setOpen(true);
    resetDir();
    resetTheme();
    resetLayout();
  };

  return (
    <>
      <SheetTrigger>
        <Button size="icon-lg" variant="ghost" aria-label="Open theme settings" className="rounded-full">
          <Settings aria-hidden="true" />
        </Button>
        <Sheet>
          <SheetHeader className="pb-0 text-start">
            <SheetTitle>Theme Settings</SheetTitle>
            <SheetDescription>Adjust the appearance and layout to suit your preferences.</SheetDescription>
          </SheetHeader>
          <div className="space-y-6 overflow-y-auto px-4">
            <ThemeConfig />
            <SidebarConfig />
            <LayoutConfig />
            <DirectionConfig />
          </div>
          <SheetFooter className="gap-2">
            <Button variant="destructive" onClick={handleReset} aria-label="Reset all settings to default values">
              Reset
            </Button>
          </SheetFooter>
        </Sheet>
      </SheetTrigger>
    </>
  );
}
