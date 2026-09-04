import { useDialog } from "@/shared/context/dialog-context";
import { useGetPagedTables } from "../tables.queries";
import { Spinner } from "@/shared/components/ui/spinner";
import { AlertError } from "@/shared/components/common/alert-error";
import { Table, TableBody, TableCell, TableHead, TableHeader, TableRow } from "@/shared/components/ui/table";
import { Button } from "@/shared/components/ui/button";


export function TablesPage() {
  const { data, isLoading, isError, error } = useGetPagedTables();
  const { openDialog } = useDialog();

  if (isLoading) return <Spinner />;
  if (isError) return <AlertError title={error.name} message={error.message} />;
  console.log('TablesPage', data);
  return (
    <>
      <div className="mb-2 flex items-center justify-between space-y-2">
        <h1 className="text-2xl font-bold tracking-tight">Tables</h1>
        <Button onClick={() => openDialog({ type: 'table-create', payload: undefined })}>
          Create Table
        </Button>
      </div>
      <Table>
        <TableHeader>
          <TableHead>Name</TableHead>
          <TableHead className="text-right">Capacity</TableHead>
          <TableHead>Status</TableHead>

        </TableHeader>
        <TableBody>
          {data?.items.map((item: any) => (
            <TableRow key={item.id}>
              <TableCell>{item.name}</TableCell>
              <TableCell className="text-right">{item.capacity}</TableCell>
              <TableCell>{item.status}</TableCell>
            </TableRow>
          ))}
        </TableBody>
      </Table>
    </>
  );
}
