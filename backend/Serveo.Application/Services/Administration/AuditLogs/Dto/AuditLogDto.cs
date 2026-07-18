namespace Serveo.Application.Services.Administration.AuditLogs.Dto
{
    public class AuditLogDto
    {
        public long Id { get; set; }
        public int? TenantId { get; set; }
        public long? UserId { get; set; }
        public string TableName { get; set; } = default!;
        public string Action { get; set; } = default!;
        public string KeyValues { get; set; } = default!;
        public string? OldValues { get; set; }
        public string? NewValues { get; set; }
        public string? ChangedColumns { get; set; }
        public DateTime CreationTime { get; set; } = DateTime.UtcNow;
    }
}
