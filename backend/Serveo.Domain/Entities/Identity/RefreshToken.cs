using Serveo.Domain.Entities.Base;

namespace Serveo.Domain.Entities.Identity
{
    public sealed class RefreshToken : Entity, IMustHaveUser, IHasCreatedAt
    {
        public Guid UserId { get; set; }


        public string TokenHash { get; set; } = default!;
        public string? ReplacedByTokenHash { get; set; }
        public DateTimeOffset ExpiresAt { get; set; }

        public DateTimeOffset CreatedAt { get; set; }
        public string? CreatedByIp { get; set; } // ghi lại nơi login tạo token

        public DateTimeOffset? RevokedAt { get; set; }
        public string? RevokedByIp { get; set; } // ghi lại nơi logout / revoke xảy ra


        public bool IsExpired => DateTime.UtcNow >= ExpiresAt;
        public bool IsRevoked => RevokedAt != null;
        public bool IsActive => !IsExpired && !IsRevoked;
    }
}
