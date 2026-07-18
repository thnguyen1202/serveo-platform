export function ActivityFeed() {
  const activities = [
    {
      text: 'Created tenant Demo',
    },

    {
      text: 'Updated role Admin',
    },
  ];

  return (
    <div>
      <h3>Recent Activity</h3>

      {activities.map((x, i) => (
        <div key={i}>{x.text}</div>
      ))}
    </div>
  );
}
