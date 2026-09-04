import { useQuery } from '@tanstack/react-query';
import { getPagedTable } from './tables.api';

// Key query duy nhất
const QUERY_KEY = 'tables';

// get paged
export function useGetPagedTables() {
  return useQuery({
    queryKey: [QUERY_KEY],
    queryFn: getPagedTable,
  });
}