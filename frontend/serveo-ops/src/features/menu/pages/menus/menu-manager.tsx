import { Button } from '@/components/ui/button';
import { SidebarMenuButton, useSidebar } from '@/components/ui/sidebar';
import { ChevronsUpDown, Plus, PlusIcon, SquareMenu } from 'lucide-react';
import { useEffect, useState } from 'react';
import { useMenus } from '../../core/hook';
import { Dialog, DialogDescription, DialogHeader, DialogTitle } from '@/components/ui/dialog';
import { Item, ItemActions, ItemContent, ItemDescription, ItemGroup, ItemMedia, ItemTitle } from '@/components/ui/item';
import { Avatar, AvatarFallback, AvatarImage } from '@/components/ui/avatar';
import { useGetMenuOptions } from '../../hooks/use-menu';
import { Spinner } from '@/components/ui/spinner';
import { AlertError } from '@/components/common/alert-error';
import { DropdownMenu, DropdownMenuGroup, DropdownMenuItem, DropdownMenuTrigger } from '@/components/ui/dropdown-menu';
import type { Selection } from "@react-types/shared";

export function MenuManager() {
  // const [isOpen, setIsOpen] = React.useState(false);
  const { open, setOpen } = useMenus();
  const isOpen = open === 'create';
  return (
    <>
      <Dialog>
        <DialogHeader>
          <DialogTitle>Are you absolutely sure?</DialogTitle>
          <DialogDescription>
            This action cannot be undone. This will permanently delete your account and remove your data from our
            servers.
          </DialogDescription>
        </DialogHeader>
      </Dialog>
      <SidebarMenuButton
        size="lg"
        variant="default"
        className="bg-secondary hover:bg-[color-mix(in_oklch,var(--secondary),var(--foreground)_5%)]"
      >
        <div
          className="
                flex aspect-square size-8
                items-center justify-center
              "
        >
          <Plus size={48} className="size-8" />
        </div>

        <div
          className="
                grid flex-1 text-start
                text-sm leading-tight
              "
        >
          <span className="truncate font-semibold">Create menu</span>

          <span className="truncate text-xs"></span>
        </div>
      </SidebarMenuButton>
      <Button className="space-x-1" onPress={() => setOpen('create')}>
        {open}
        <span>Create</span> <Plus size={18} />
      </Button>
      <MenuDropdown />
      <ItemGroupExample />
      {/* <Collapsible isExpanded={isOpen} onExpandedChange={setIsOpen} className="flex w-87.5 flex-col gap-2">
        <div className="flex items-center justify-between gap-4 px-4">
         
          <Button slot="trigger" variant="ghost" size="icon" className="size-8">
             <h4 className="text-sm font-semibold">Order #4189</h4>
            <ChevronsUpDown />
            <span className="sr-only">Toggle details</span>
          </Button>
        </div>
        <div className="flex items-center justify-between rounded-md border px-4 py-2 text-sm">
          <span className="text-muted-foreground">Status</span>
          <span className="font-medium">Shipped</span>
        </div>
        <CollapsibleContent>
          <div className="flex flex-col gap-2">
            <div className="rounded-md border px-4 py-2 text-sm">
              <p className="font-medium">Shipping address</p>
              <p className="text-muted-foreground">100 Market St, San Francisco</p>
            </div>
            <div className="rounded-md border px-4 py-2 text-sm">
              <p className="font-medium">Items</p>
              <p className="text-muted-foreground">2x Studio Headphones</p>
            </div>
          </div>
        </CollapsibleContent>
      </Collapsible> */}
    </>
  );
}

function MenuDropdown() {
  const { data = [], isLoading, isError, error } = useGetMenuOptions();

  const { isMobile } = useSidebar();
  const [selectedKeys, setSelectedKeys] = useState<Selection>(new Set());
  const selectedId1 = [...selectedKeys][0] as string | undefined;
  const selected = data.find(x => x.id === selectedId1);

  useEffect(() => {
    if (data.length === 0) return;

    setSelectedKeys(prev => {
      // Đã có selection hợp lệ → giữ nguyên
      const currentId = [...prev][0] as string | undefined;

      if (currentId && data.some(x => x.id === currentId)) {
        return prev;
      }

      // Chưa có hoặc selection không còn tồn tại → chọn item đầu tiên
      return new Set([data[0].id]);
    });
  }, [data]);

  if (isLoading) return <Spinner />;
  if (isError) return <AlertError title={error.name} message={error.message} />;

  const handleSelectionChange = (keys: Selection) => {
    if (keys === 'all') return;

    if (![...keys][0]) return; // khong duoc null

    setSelectedKeys(keys);
  };

  return (
    <>
      <DropdownMenuTrigger>
        <SidebarMenuButton
          size="lg"
          className="
              data-[state=open]:bg-sidebar-accent
              data-[state=open]:text-sidebar-accent-foreground
            "
        >
          <div
            className="
                flex aspect-square size-8
                items-center justify-center
                rounded-lg
                bg-sidebar-primary
                text-sidebar-primary-foreground
              "
          >
            <SquareMenu className="size-41" />
          </div>

          <div
            className="
                grid flex-1 text-start
                text-sm leading-tight
              "
          >
            <span className="truncate font-semibold">{selected?.name}</span>

            <span className="truncate text-xs text-muted-foreground">{selected?.itemCount} items</span>
          </div>

          <ChevronsUpDown className="ms-auto" />
        </SidebarMenuButton>

        <DropdownMenu
          placement={isMobile ? 'bottom' : 'bottom'}
          className="
                w-(--trigger-width)
                min-w-56
                rounded-lg
              "
        >
          <DropdownMenuGroup
            selectionMode="single"
            selectedKeys={selectedKeys}
            onSelectionChange={handleSelectionChange}
          >
            {data.map((item) => {
              return (
                <DropdownMenuItem id={item.id} key={item.id} className="gap-2 p-2 active">
                  <div className="flex size-6 items-center justify-center rounded-sm border">
                    <SquareMenu className="size-4 shrink-0" />
                  </div>
                  <div className="grid flex-1 text-start text-sm leading-tight">
                    <span className="truncate font-medium">{item?.name}</span>
                    <span className="truncate text-xs text-muted-foreground">{item?.itemCount} items</span>
                  </div>
                </DropdownMenuItem>
              );
            })}
          </DropdownMenuGroup>
        </DropdownMenu>
      </DropdownMenuTrigger>

    </>);
}

const people = [
  {
    username: 'alex',
    avatar: '/avatars/01.png',
    email: 'alex@example.com',
  },
  {
    username: 'jamie',
    avatar: '/avatars/02.png',
    email: 'jamie@example.com',
  },
  {
    username: 'taylor',
    avatar: '/avatars/03.png',
    email: 'taylor@example.com',
  },
];
export function ItemGroupExample() {

  return (
    <div className="flex w-full flex-col gap-6">
      <ItemGroup className="grid grid-cols-3 gap-4">
        {people.map((person, index) => (
          <Item key={person.username} variant="outline">
            <ItemMedia>
              <Avatar>
                <AvatarImage src={person.avatar} className="grayscale" />
                <AvatarFallback>{person.username.charAt(0)}</AvatarFallback>
              </Avatar>
            </ItemMedia>
            <ItemContent className="gap-1">
              <ItemTitle>{person.username}</ItemTitle>
              <ItemDescription>{person.email}</ItemDescription>
            </ItemContent>
            <ItemActions>
              <Button variant="ghost" size="icon" className="rounded-full">
                <PlusIcon />
              </Button>
            </ItemActions>
          </Item>
        ))}
      </ItemGroup>
    </div>
  );
}
