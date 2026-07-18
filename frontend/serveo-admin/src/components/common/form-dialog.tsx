import { Dialog, DialogContent, DialogHeader, DialogTitle } from '../ui/dialog';

interface Props {
  open: boolean;
  title: string;
  children: React.ReactNode;
}

export function FormDialog({ open, title, children }: Props) {
  return (
    <Dialog open={open}>
      <DialogContent>
        <DialogHeader>
          <DialogTitle>{title}</DialogTitle>
        </DialogHeader>

        {children}
      </DialogContent>
    </Dialog>
  );
}
