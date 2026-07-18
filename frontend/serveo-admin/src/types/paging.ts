export interface PagedResult<T> {
  items: T[];
  page: number;
  pageSize: number;
  total: number;
}

export interface PageFilter {
  page: number;
  size: number;
  search?: string;
}
