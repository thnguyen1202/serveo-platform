import type { VisibilityState } from '@tanstack/react-table';

export function loadColumnPreference(key: string): VisibilityState {
  const value = localStorage.getItem(key);

  if (!value) {
    return {};
  }

  try {
    return JSON.parse(value);
  } catch {
    return {};
  }
}

export function saveColumnPreference(key: string, value: VisibilityState) {
  localStorage.setItem(key, JSON.stringify(value));
}
