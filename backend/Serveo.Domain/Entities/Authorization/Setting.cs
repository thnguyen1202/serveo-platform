using Serveo.Domain.Entities.Base;

namespace Serveo.Domain.Entities.Authorization
{
    public sealed class Setting : Entity, IMayHaveTenant, IMayHaveUser
    {
        public Guid? TenantId { get; set; }
        public Guid? UserId { get; set; }

        public string Name { get; set; } = default!;
        public string? Value { get; set; }
    }
}
