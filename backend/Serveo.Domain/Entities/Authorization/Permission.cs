using Serveo.Domain.Entities.Base;
using Serveo.Domain.Entities.Identity;

namespace Serveo.Domain.Entities.Authorization
{
    public sealed class Permission : Entity, IMayHaveTenant
    {
        public Guid RoleId { get; set; }
        public Guid? TenantId { get; set; }

        public string Name { get; set; } = default!;

        public bool IsGranted { get; set; }


        #region Relationships
        public RefreshToken Role { get; set; } = default!;
        #endregion
    }
}
