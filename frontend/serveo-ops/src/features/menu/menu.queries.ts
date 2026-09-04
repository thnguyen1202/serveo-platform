import { useQuery } from '@tanstack/react-query';
import { getMenuDetails, getMenuOptions, getPaged } from './menu.api';

// Key query duy nhất
const QUERY_KEY = 'menus';

// Hook lấy menu dropdown data
export function useGetMenuOptions() {
  return useQuery({
    queryKey: [QUERY_KEY, {}],
    queryFn: ({ signal }) => getMenuOptions({ signal }),
    staleTime: Infinity,
  });
}

export function usePagingQuery() {
  return useQuery({
    queryKey: ['menus'],
    queryFn: getPaged,
  });
}

export function useMenuDetails(menuId: string, full: boolean) {
  return useQuery({
    queryKey: ['menus', menuId, full],
    queryFn: () => getMenuDetails(menuId,full),
    staleTime: Infinity
  });
}