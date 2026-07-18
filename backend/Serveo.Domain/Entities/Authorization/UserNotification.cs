using Serveo.Domain.Entities.Base;

namespace Serveo.Domain.Entities.Authorization
{
    public sealed class UserNotification : CreationAuditableEntity, IMayHaveTenant, IMustHaveUser
    {
        public Guid UserId { get; set; }
        public Guid? TenantId { get; set; }

        public Guid TenantNotificationId { get; set; }

        public UserNotificationState State { get; set; }

        public string? TargetNotifiers { get; set; }
    }

    public enum UserNotificationState
    {
        Unread = 0,
        Read
    }
}
