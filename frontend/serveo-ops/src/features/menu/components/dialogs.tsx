import { useDialog } from "../../../shared/context/dialog-context";
import { CategoryCreateDialog } from "./category-create-dialog";
import { MenuCreateDialog } from "./menu-create-dialog";
import { ProductCreateDialog } from "./product-create-dialog";

export function Dialogs() {
    const { dialog } = useDialog();

    if (!dialog) {
        return null;
    }
    switch (dialog.type) {
        case "menu-create":
            return <MenuCreateDialog />;
        case "category-create":
            return <CategoryCreateDialog />;
        case "product-create":
            return <ProductCreateDialog />;
        default:
            return null;
    }
}
// function AppDialogs() {
//   const { dialog } = useDialog();

//   if (!dialog) {
//     return null;
//   }

//   switch (dialog.type) {
//     case "menu-create":
//       return <MenuCreateDialog />;

//     case "menu-edit":
//       return (
//         <MenuEditDialog
//           menu={dialog.payload}
//         />
//       );

//     case "category-create":
//       return <CategoryCreateDialog />;

//     case "category-edit":
//       return (
//         <CategoryEditDialog
//           category={dialog.payload}
//         />
//       );

//     case "product-create":
//       return <ProductCreateDialog />;

//     case "product-edit":
//       return (
//         <ProductEditDialog
//           product={dialog.payload}
//         />
//       );

//     default:
//       return null;
//   }
// }