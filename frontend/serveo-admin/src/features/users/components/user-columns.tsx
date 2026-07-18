export const columns = [
  {
    accessorKey: 'username',
    header: 'Username',
  },

  {
    accessorKey: 'email',
    header: 'Email',
  },

  {
    accessorKey: 'roles',
    header: 'Roles',

    cell: ({ row }) => row.original.roles.join(', '),
  },

  {
    accessorKey: 'active',
    header: 'Status',
  },
];
