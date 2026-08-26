using Serveo.Domain.Entities.Identity;

namespace Serveo.Domain.Entities.Authorization
{
    public class RolePermission
    {
        public Guid RoleId { get; private set; }
        public Guid PermissionId { get; private set; }


        public Role Role { get; private set; } = null!;
        public Permission Permission { get; private set; } = null!;
    }
}
