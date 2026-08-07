import { getTokenExpiresAt } from '@/lib/jwt';

const ACCESS_TOKEN_KEY = 'serveo_access_token';
const REFRESH_TOKEN_KEY = 'serveo_refresh_token';
const EXPIRES_IN_KEY = 'serveo_expires_in';

export const tokenStorage = {
  getAccessToken() {
    return localStorage.getItem(ACCESS_TOKEN_KEY);
  },

  setAccessToken(token: string) {
    localStorage.setItem(ACCESS_TOKEN_KEY, token);
  },

  getRefreshToken() {
    return localStorage.getItem(REFRESH_TOKEN_KEY);
  },

  setRefreshToken(token: string) {
    localStorage.setItem(REFRESH_TOKEN_KEY, token);
  },

  getExpiresAt() {
    const expiresAt = localStorage.getItem(EXPIRES_IN_KEY);
    return expiresAt ? parseInt(expiresAt, 10) : null;
  },

  setExpiresAt(expiresAt: number | null) {
    if (expiresAt === null) {
      localStorage.removeItem(EXPIRES_IN_KEY);
      return;
    }
    localStorage.setItem(EXPIRES_IN_KEY, expiresAt.toString());
  },

  clear() {
    localStorage.removeItem(ACCESS_TOKEN_KEY);
    localStorage.removeItem(REFRESH_TOKEN_KEY);
    localStorage.removeItem(EXPIRES_IN_KEY);
  },

  setTokens(accessToken: string, refreshToken: string) {
    const expiresAt = getTokenExpiresAt(accessToken);
    this.setAccessToken(accessToken);
    this.setRefreshToken(refreshToken);
    this.setExpiresAt(expiresAt);
  },

  isExpiringSoon() {
    const expiresAt = this.getExpiresAt();
    if (!expiresAt) return false;

    const remaining = expiresAt - Date.now();
    return remaining < 60_000; // less than 1 minute
  },
};
