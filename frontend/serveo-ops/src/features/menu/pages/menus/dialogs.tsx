import { useMenus } from '../../core/hook';
import { CreateUpdateMenuDialog as CreateUpdateDialog } from './create-dialog';

export function MenusDialogs() {
  return (
    <>
      <CreateUpdateDialog key="menu-add" />
      {/* <UpdateTaskDrawer />
      <DeleteTaskDialog /> */}

      {/* <CreateTaskDrawer
        open={open === "create"}
        onOpenChange={(isOpen) => !isOpen && setOpen(null)}
      />

      <UpdateTaskDrawer
        open={open === "update"}
        onOpenChange={(isOpen) => !isOpen && setOpen(null)}
      />

      <DeleteTaskDialog
        open={open === "delete"}
        onOpenChange={(isOpen) => !isOpen && setOpen(null)}
      /> */}
    </>
  );
}
