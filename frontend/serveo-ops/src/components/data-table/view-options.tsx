// import { Settings2 } from 'lucide-react';
// import type { Table } from '@tanstack/react-table';

// import {
//   DropdownMenu,
//   DropdownMenuCheckboxItem,
//   DropdownMenuContent,
//   DropdownMenuTrigger,
// } from '@/components/ui/dropdown-menu';

// import { Button } from '@/components/ui/button';

// interface DataTableViewOptionsProps<TData> {
//   table: Table<TData>;
// }

// export function DataTableViewOptions<TData>({ table }: DataTableViewOptionsProps<TData>) {
//   return (
//     <DropdownMenu>
//       <DropdownMenuTrigger asChild>
//         <Button variant="outline" size="sm" className="ml-auto h-8">
//           <Settings2 />
//           View
//         </Button>
//       </DropdownMenuTrigger>

//       <DropdownMenuContent align="end" className="w-[180px]">
//         {table
//           .getAllColumns()
//           .filter((column) => typeof column.accessorFn !== 'undefined' && column.getCanHide())
//           .map((column) => (
//             <DropdownMenuCheckboxItem
//               key={column.id}
//               className="capitalize"
//               checked={column.getIsVisible()}
//               onCheckedChange={(value) => column.toggleVisibility(value)}
//             >
//               {column.id}
//             </DropdownMenuCheckboxItem>
//           ))}
//       </DropdownMenuContent>
//     </DropdownMenu>
//   );
// }
