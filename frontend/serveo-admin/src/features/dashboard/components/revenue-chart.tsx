import { Line, LineChart, Tooltip, XAxis, YAxis } from 'recharts';

export function RevenueChart() {
  const data = [
    {
      date: '01/01',
      revenue: 1000,
    },

    {
      date: '02/01',
      revenue: 1500,
    },
  ];

  return (
    <LineChart width={500} height={300} data={data}>
      <XAxis dataKey="date" />

      <YAxis />

      <Tooltip />

      <Line dataKey="revenue" />
    </LineChart>
  );
}
