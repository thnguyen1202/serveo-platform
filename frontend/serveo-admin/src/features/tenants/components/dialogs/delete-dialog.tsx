import type { DialogProps } from 'react-aria-components';
import { useTenants } from '../../core/hook';

export function DeleteTaskDialog(props: DialogProps) {
  const { currentRow } = useTenants();
}
