import { useState } from 'react';

import { PageHeader } from '@/components/common/page-header';

import { UserTable } from '../components/user-table';
import { useUsers } from '../hooks/use-users';
import type { UserPageRequest } from '../types/user.types';

export default function UsersPage() {
  const [filter] = useState<UserPageRequest>({
    page: 1,
    size: 20,
  });
  const { data, isLoading, error } = useUsers(filter);

  if (isLoading) {
    return <div>Loading...</div>;
  }

  if (error) {
    // return <ErrorState message={error.message} />;
    return <div className="text-error">{error.message}</div>;
  }

  return (
    <div className="space-y-6">
      <PageHeader
        title="Users"
        description="Manage system users"
        // action={<CreateUserButton />}
      />

      {/* <UserFilter value={filter} onChange={setFilter} /> */}
      {/* <UserFilter /> */}

      <UserTable data={data?.items ?? []} />
    </div>
  );

  // return (
  //   <div>
  //     {data?.map((user) => (
  //       <div key={user.id}>{user.username}</div>
  //     ))}
  //   </div>
  // );

  // return (
  //   <div>
  //     <PageHeader title="Users" description="Manage system users" action={<Button>Create User</Button>} />
  //     <PermissionGuard permission="users.create">
  //       <Button>Create</Button>
  //     </PermissionGuard>
  //     <PermissionGuard permission="users.delete">
  //       <Button>Delete</Button>
  //     </PermissionGuard>
  //     <UserTable />
  //   </div>
  // );
}
