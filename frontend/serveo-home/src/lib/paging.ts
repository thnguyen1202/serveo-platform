export interface PagedResult<T> {
  items: T[];
  page: number;
  pageSize: number;
  total: number;
  itemCount: number;
}

export interface PagingFilter {
  page: number;
  size: number;
  search?: string;
}
