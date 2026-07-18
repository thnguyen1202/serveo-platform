import type { PageFilter } from '@/types/paging';

export interface User {
  id: string;
  username: string;
  email: string;
  displayName: string;
  active: boolean;
  roles: string[];
  createdAt: string;
}

export interface CreateUserRequest {
  username: string;
  email: string;
  password: string;
  displayName: string;
}

export interface UserPageRequest extends PageFilter {
  status?: 'active' | 'inactive';
  roleId?: string;
}
