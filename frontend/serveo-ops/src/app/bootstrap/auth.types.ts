export interface AuthUserResponse {
  id: string;
  username: string;
  email: string;
  displayName: string;
  tenantId?: string;
  branchId?: string;
  permissions: string[];
}

export interface AuthTokenResponse {
  accessToken: string;
}

export type CsrfResponse = {
  csrfToken: string;
};
