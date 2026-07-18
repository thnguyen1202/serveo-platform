export interface LoginRequest {
  email: string;
  password: string;
}

export interface LoginResponse {
  accessToken: string;
  refreshToken: string;
}

export interface UserInfo {
  id: string;
  username: string;
  email: string;
  displayName: string;
  tenantId?: string;
  branchId?: string;
  permissions: string[];
}
