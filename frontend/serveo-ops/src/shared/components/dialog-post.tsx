import type { ReactNode } from "react";
import { ButtonSubmit } from "./common/button-submit";
import { Dialog, DialogClose, DialogDescription, DialogFooter, DialogHeader, DialogTitle } from "./ui/dialog";
import { useTranslation } from "react-i18next";

interface DialogPostProps {
    /** Trạng thái đóng/mở của Dialog */
    isOpen: boolean;
    /** Callback xử lý khi trạng thái đóng/mở thay đổi */
    onOpenChange: (open: boolean) => void;
    /** Tiêu đề của Dialog */
    dialogTitle: ReactNode;
    /** Mô tả phụ bên dưới tiêu đề */
    dialogDescription?: ReactNode;
    /** ID kết nối giữa Form và ButtonSubmit */
    formId: string;
    /** Trạng thái loading khi đang submit form */
    isPending?: boolean;
    /** Nội dung Form hoặc body chính của Dialog */
    children: ReactNode;
}

export function DialogPost({
    isOpen,
    onOpenChange,
    dialogTitle,
    dialogDescription,
    formId,
    isPending = false,
    children,
}: DialogPostProps) {
    const { t } = useTranslation('common');

    return (
        <Dialog className="sm:max-w-sm" isOpen={isOpen} onOpenChange={onOpenChange}>
            <DialogHeader>
                <DialogTitle>{dialogTitle}</DialogTitle>
                {dialogDescription && (
                    <DialogDescription>{dialogDescription}</DialogDescription>
                )}
            </DialogHeader>

            {/* React renders nội dung truyền vào giữa 2 thẻ DialogPost ở đây */}
            {children}

            <DialogFooter>
                <DialogClose variant="outline">{t('cancel')}</DialogClose>
                <ButtonSubmit form={formId} isPending={isPending} text={t('save')} />
            </DialogFooter>
        </Dialog>
    );
}