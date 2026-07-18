using Serveo.Domain.Entities.Base;

namespace Serveo.Domain.Entities.Authorization
{
    public sealed class TenantNotification : CreationAuditableEntity, IMustHaveTenant
    {
        public Guid TenantId { get; set; }

        public string NotificationName { get; set; } = default!;
        public string? Data { get; set; }
        public string? DataTypeName { get; set; }
        public string? EntityTypeName { get; set; }
        public string? EntityTypeAssemblyQualifiedName { get; set; }
        public string? EntityId { get; set; }

        public NotificationSeverity Severity { get; set; }
    }

    public enum NotificationSeverity : byte
    {
        Info = 0,
        Success = 1,
        Warn = 2,
        Error = 3,
        Fatal = 4
    }
}
