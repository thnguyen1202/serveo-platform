import { useDialog } from "@/shared/context/dialog-context";
import { TableCreateDialog } from "./table-create-dialog";

export function Dialogs() {
    const { dialog } = useDialog();

    if (!dialog) {
        return null;
    }
    switch (dialog.type) {
        case "table-create":
            return <TableCreateDialog />;
        default:
            return null;
    }
}