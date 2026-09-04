import { Button } from '@/shared/components/ui/button';
import {
  DropdownMenu,
  DropdownMenuGroup,
  DropdownMenuItem,
  DropdownMenuLabel,
  DropdownMenuSeparator,
  DropdownMenuShortcut,
  DropdownMenuTrigger,
} from '@/shared/components/ui/dropdown-menu';
import { Avatar, AvatarFallback, AvatarImage } from './ui/avatar';
import { LogOutIcon, SettingsIcon, UserIcon } from 'lucide-react';
import { LogoutDialog } from './logout-dialog';
import useDialogState from '@/shared/hooks/use-dialog-state';
import { Kbd } from './ui/kbd';

export function ProfileDropdown() {
  const [open, setOpen] = useDialogState();

  return (
    <>
      <DropdownMenuTrigger>
        <Button variant="ghost" className="relative h-8 w-8 rounded-full">
          <Avatar className="h-8 w-8">
            <AvatarImage src="/avatars/01.png" alt="@shadcn" />
            <AvatarFallback>SN</AvatarFallback>
          </Avatar>
        </Button>
        <DropdownMenu className="w-56">
          <DropdownMenuGroup>
            <DropdownMenuLabel className="font-normal">
              <div className="flex flex-col gap-1.5">
                <p className="text-sm leading-none font-medium">satnaing</p>
                <p className="text-xs leading-none text-muted-foreground">satnaingdev@gmail.com</p>
              </div>
            </DropdownMenuLabel>
          </DropdownMenuGroup>

          <DropdownMenuSeparator />

          <DropdownMenuGroup>
            <DropdownMenuItem href="/profile">
              <UserIcon />
              Profile
              <DropdownMenuShortcut>⇧⌘P</DropdownMenuShortcut>
            </DropdownMenuItem>
            <DropdownMenuItem href="/settings">
              <SettingsIcon />
              Settings
              <DropdownMenuShortcut>⌘S</DropdownMenuShortcut>
            </DropdownMenuItem>
          </DropdownMenuGroup>

          <DropdownMenuSeparator />

          <DropdownMenuGroup>
            <DropdownMenuItem variant="destructive" onClick={() => setOpen(true)}>
              <LogOutIcon />
              Log out
              <DropdownMenuShortcut className="text-current">
                <Kbd>⇧⌘Q</Kbd>
              </DropdownMenuShortcut>
            </DropdownMenuItem>
          </DropdownMenuGroup>
        </DropdownMenu>
      </DropdownMenuTrigger>

      <LogoutDialog isOpen={!!open} onOpenChange={setOpen} />
    </>
  );
}
