export interface AuthUser {
  id: string;
  username: string;
  email: string;
  displayName: string;
  tenantId?: string;
  branchId?: string;
  permissions: string[];
}
