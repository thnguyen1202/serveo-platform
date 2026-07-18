import { Input } from '@/components/ui/input';
import { Select, SelectTrigger } from '@/components/ui/select';

export function UserFilter() {
  return (
    <div className="flex gap-3">
      <Input placeholder="Search users" />

      <Select>
        <SelectTrigger>Status</SelectTrigger>
      </Select>
    </div>
  );
}
