import { useQuery } from "@tanstack/react-query";
import { getMenuOptions } from "../api/menu.api";

// Key query duy nhất
const QUERY_KEY = 'menus';

// Hook lấy menu dropdown data
export function useGetMenuOptions() {
  return useQuery({
    queryKey: [QUERY_KEY, {  }],
    queryFn: ({ signal }) => getMenuOptions({ signal }),
  });
}