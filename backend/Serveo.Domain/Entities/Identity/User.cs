using Microsoft.AspNetCore.Identity;
using Serveo.Domain.Entities.Base;

namespace Serveo.Domain.Entities.Identity
{
    public sealed class User : IdentityUser<Guid>, IAuditable, IMayHaveTenant
    {
        public Guid? TenantId { get; set; }
        public Guid? BusinessId { get; set; }
        public Guid? BranchId { get; set; }

        public string? DisplayName { get; set; }
        public string? Avatar { get; set; }
        public string? TimeZoneId { get; set; }

        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? ModifiedAt { get; set; }

        public UserStatus Status { get; set; }

        public void SetNormalizedNames()
        {
            NormalizedUserName = UserName?.ToUpperInvariant();
            NormalizedEmail = Email?.ToUpperInvariant();
        }

        public void SetSecurityStamp() => SecurityStamp = GenerateSecurityStamp();

        private readonly Func<string> GenerateSecurityStamp = delegate ()
        {
            return string.Concat(Array.ConvertAll(Guid.NewGuid().ToByteArray(), b => b.ToString("X2")));
        };
    }

    public enum UserStatus : byte
    {
        Active = 1,
        Suspended = 2,
        Locked = 3,
        Inactive = 4
    }
}
