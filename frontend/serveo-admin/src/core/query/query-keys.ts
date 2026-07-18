export const queryKeys = {
  users: {
    all: ['users'],
    list: (filter: unknown) => ['users', filter],
    detail: (id: string) => ['users', id],
  },

  roles: {
    all: ['roles'],
  },

  tenants: {
    all: ['tenants'],
  },
};
