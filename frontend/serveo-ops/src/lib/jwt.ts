import { jwtDecode, type JwtPayload } from 'jwt-decode';

export function getTokenExpiresAt(token: string): number | null {
  try {
    const { exp } = jwtDecode<JwtPayload>(token);
    return exp ? exp * 1000 : null;
  } catch {
    return null;
  }
}
