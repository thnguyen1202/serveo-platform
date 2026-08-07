import { Plus } from 'lucide-react';
import { Button } from '@/components/ui/button';
import { useTenants } from '../core/hook';

export function TenantsPrimaryButtons() {
  const { setOpen } = useTenants();
  return (
    <div className="flex gap-2">
      <Button className="space-x-1" onPress={() => setOpen('create')}>
        <span>Create</span> <Plus size={18} />
      </Button>
    </div>
  );
}
