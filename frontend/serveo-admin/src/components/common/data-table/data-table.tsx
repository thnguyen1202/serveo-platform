import type { Column } from './columns';

interface Props<T> {
  columns: Column[];
  data: T[];
}

export function DataTable<T>({ columns, data }: Props<T>) {
  return (
    <table>
      <thead>
        <tr>
          {columns.map((column) => (
            <th>{column.header}</th>
          ))}
        </tr>
      </thead>

      <tbody>
        {data.map((item, index) => (
          <tr key={index}>
            {columns.map((column) => (
              <td>{item[column.accessorKey]}</td>
            ))}
          </tr>
        ))}
      </tbody>
    </table>
  );
}
