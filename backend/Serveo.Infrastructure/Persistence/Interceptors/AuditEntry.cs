using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Serveo.Domain.Entities.Authorization;
using System.Text.Json;

namespace Serveo.Infrastructure.Persistence.Interceptors
{
    internal class AuditEntry(EntityEntry entry)
    {
        public EntityEntry Entry { get; } = entry;

        public string TableName { get; } = entry.Metadata.GetTableName()!;
        public string Action { get; } = entry.State.ToString();

        public Dictionary<string, object?> KeyValues { get; } = [];
        public Dictionary<string, object?> OldValues { get; } = [];
        public Dictionary<string, object?> NewValues { get; } = [];
        public List<string> ChangedColumns { get; } = [];

        public List<PropertyEntry> TemporaryProperties { get; } = [];

        public bool HasTemporaryProperties => TemporaryProperties.Count > 0;

        public AuditLog ToAuditLog(Guid? tenantId, Guid? userId)
        {
            return new AuditLog
            {
                TenantId = tenantId,
                UserId = userId,
                TableName = TableName,
                Action = Action,
                KeyValues = JsonSerializer.Serialize(KeyValues),
                OldValues = OldValues.Count == 0 ? null : JsonSerializer.Serialize(OldValues),
                NewValues = NewValues.Count == 0 ? null : JsonSerializer.Serialize(NewValues),
                ChangedColumns = ChangedColumns.Count == 0 ? null : JsonSerializer.Serialize(ChangedColumns)
            };
        }
    }
}
