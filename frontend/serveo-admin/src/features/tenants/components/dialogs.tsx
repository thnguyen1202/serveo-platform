import { CreateTenantDrawer } from './dialogs/create-dialog';

export function TenantsDialogs() {
  return (
    <>
      <CreateTenantDrawer />
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
