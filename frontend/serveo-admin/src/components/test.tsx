import { Settings, User } from 'lucide-react';

import { Button } from '@/components/ui/button';

export default function Test() {
  return (
    <div>
      Test: Icon Library : lucide-react
      <User />
      <Settings />
      <hr />
      <Button>Create</Button>
      <Button>Save</Button>
      <Button>Submit</Button>
      <Button variant="secondary">Cancel</Button>
      <Button variant="secondary">Filter</Button>
      <Button variant="secondary">Export</Button>
      <Button variant="destructive">Delete</Button>
      <Button variant="destructive">Remove</Button>
      <hr />
      <Button variant="default">default</Button>
      <Button variant="destructive">destructive</Button>
      <Button variant="ghost">ghost</Button>
      <Button variant="link">link</Button>
      <Button variant="outline">outline</Button>
      <Button variant="secondary">secondary</Button>
    </div>
  );
}
