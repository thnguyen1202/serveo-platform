import { useQuery } from '@tanstack/react-query';
import { getPaged } from './api';

export function usePagingQuery() {
  return useQuery({
    queryKey: ['menus'],
    queryFn: getPaged,
  });
}
