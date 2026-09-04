import { SidebarMenuButton } from "@/shared/components/ui/sidebar";
import { ChevronsUpDown, CirclePlus, Plus, Settings, SquareMenu } from "lucide-react";
import { useGetMenuOptions, useMenuDetails } from "../menu.queries";
import { DropdownMenu, DropdownMenuGroup, DropdownMenuItem, DropdownMenuSeparator, DropdownMenuTrigger } from "@/shared/components/ui/dropdown-menu";
import { AlertError } from "@/shared/components/common/alert-error";
import { Spinner } from "@/shared/components/ui/spinner";
import { useEffect, useState } from "react";
import type { Selection } from '@react-types/shared';
import { useDialog } from "../../../shared/context/dialog-context";
import { Button } from "react-aria-components";
import { useMenuStore } from "@/shared/hooks/use-menu-store";

export function MenuPage() {
  const { data = [], isLoading, isError, error } = useGetMenuOptions();
  const { openDialog } = useDialog();

  const isMobile = false;
  const setMenuId = useMenuStore((state) => state.setMenuId);
  const [selectedKeys, setSelectedKeys] = useState<Selection>(new Set());
  const selectedId = [...selectedKeys][0] as string | undefined;
  const selected = data.find((x) => x.id === selectedId);

  useEffect(() => {
    if (data.length === 0) return;

    setSelectedKeys((prev) => {
      // Đã có selection hợp lệ → giữ nguyên
      const currentId = [...prev][0] as string | undefined;

      if (currentId && data.some((x) => x.id === currentId)) {
        return prev;
      }

      setMenuId(data[0].id); // Cập nhật menuId vào Zustand Store

      // Chưa có hoặc selection không còn tồn tại → chọn item đầu tiên
      return new Set([data[0].id]);
    });
  }, [data]);

  if (isLoading) return <Spinner />;
  if (isError) return <AlertError title={error.name} message={error.message} />;

  const handleSelectionChange = (keys: Selection) => {
    if (keys === 'all') return;

    if (![...keys][0]) return; // prevent deselection of the item

    const id = [...keys][0] as string;
    setMenuId(id); // Cập nhật menuId vào Zustand Store

    setSelectedKeys(keys);
  };

  return (
    <>
      <Button onClick={() => openDialog({ type: "menu-create", payload: undefined })}>
        <span className="text-2xl font-bold ">Menu 1</span>
        <span className="group/button inline-flex shrink-0 items-center justify-center rounded-lg border border-transparent bg-clip-padding text-sm font-medium whitespace-nowrap transition-all outline-none select-none focus-visible:border-ring focus-visible:ring-3 focus-visible:ring-ring/50 active:not-aria-[haspopup]:translate-y-px disabled:pointer-events-none disabled:opacity-50 aria-invalid:border-destructive aria-invalid:ring-3 aria-invalid:ring-destructive/20 dark:aria-invalid:border-destructive/50 dark:aria-invalid:ring-destructive/40 [&_svg]:pointer-events-none [&_svg]:shrink-0 [&_svg:not([class*='size-'])]:size-4
      hover:bg-muted hover:text-foreground aria-expanded:bg-muted aria-expanded:text-foreground dark:hover:bg-muted/50">
          <ChevronsUpDown size={24} strokeWidth={4} />
        </span>
      </Button>


      {/* <SidebarMenuButton
        size="lg"
        className="data-[state=open]:bg-sidebar-accent
              data-[state=open]:text-sidebar-accent-foreground 
              w-auto"
      >

        <div
          className="
                grid flex-1 text-start
                text-sm leading-tight
              "
        >
          <span className="text-2xl font-bold tracking-tight">Create menu</span>
        </div>
         <ChevronsUpDown size={24} strokeWidth={4} className="ms-auto" />
      </SidebarMenuButton> */}

      <DropdownMenuTrigger>
        <SidebarMenuButton
          size="lg"
          className="
              data-[state=open]:bg-sidebar-accent
              data-[state=open]:text-sidebar-accent-foreground 
              w-auto
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
          placement={isMobile ? "bottom" : "right top"}
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
              const isSelected = selectedId === item.id;
              return (
                <DropdownMenuItem id={item.id} key={item.id} data-focused="true" className="gap-2 p-2 active">
                  <div className="flex size-6 items-center justify-center rounded-sm border">
                    <SquareMenu className="size-4 shrink-0" />
                  </div>
                  <div className="grid flex-1 text-start text-sm leading-tight">
                    <span className={`truncate ${isSelected ? "font-bold" : "font-medium"}`}>{item?.name}</span>
                    <span className={`truncate text-xs text-muted-foreground ${isSelected ? "font-semibold" : ""}`}>{item?.itemCount} items</span>
                  </div>
                </DropdownMenuItem>
              );
            })}
          </DropdownMenuGroup>

          <DropdownMenuSeparator />

          <DropdownMenuGroup>
            <DropdownMenuItem onClick={() => openDialog({ type: "menu-create", payload: undefined })} className="gap-2 p-2">
              <CirclePlus className="size-4 text-muted-foreground" />
              <div className="font-medium text-muted-foreground">Create menu</div>
            </DropdownMenuItem>

            <DropdownMenuItem className="gap-2 p-2">
              <Settings className="size-4 text-muted-foreground" />
              <div className="font-medium text-muted-foreground">Manage menus</div>
            </DropdownMenuItem>
          </DropdownMenuGroup>
        </DropdownMenu>
      </DropdownMenuTrigger>


      <SidebarMenuButton
        size="lg"
        variant="default"
        className="bg-secondary hover:bg-[color-mix(in_oklch,var(--secondary),var(--foreground)_5%)]"
        onClick={() => openDialog({ type: "category-create", payload: { menuId: selectedId } })}
      >
        <Plus strokeWidth={2.5} />
        <div
          className="
                grid flex-1 text-start
                text-sm leading-tight
              "
        >
          <span className="truncate font-medium">Create category</span>
        </div>
      </SidebarMenuButton>
      <Categories menuId={selectedId} />
    </>
  );
}

function Categories({ menuId }: { menuId: string | undefined }) {
  if (!menuId) return null;

  const { data, isLoading, isError, error } = useMenuDetails(menuId, true);
  const { openDialog } = useDialog();

  if (isLoading) return <Spinner />;
  if (isError) return <AlertError title={error.name} message={error.message} />;

  console.log('Categories', data, data.categories);
  return (
    <>
      <section >
        {data.categories.map((category: any) => (
          <div>
            <span className="text-2xl font-semibold ">{category.name}</span>
            {category.products.map((product:any)=>(
              <div>
                <span>{product.name}</span>
                <span className="ms-2">{product.price}</span>
              </div>
            ))}
            <SidebarMenuButton
              size="lg"
              variant="default"
              className="bg-secondary hover:bg-[color-mix(in_oklch,var(--secondary),var(--foreground)_5%)]"
              onClick={() => openDialog({ type: "product-create", payload: { menuId: menuId, categoryId: category.id } })}
            >
              <Plus strokeWidth={2.5} />
              <div
                className="
                grid flex-1 text-start
                text-sm leading-tight
              "
              >
                <span className="truncate font-medium">Create product</span>
              </div>
            </SidebarMenuButton>
          </div>

        ))}
      </section>
    </>
  );
}