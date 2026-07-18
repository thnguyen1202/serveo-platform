using Serveo.Domain.Entities.Identity;

namespace Serveo.Application.Dtos.Identity
{
    public sealed class UserDto : EntityDto
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
    }
}
