using Microsoft.EntityFrameworkCore;
using Serveo.Application.Abstractions;

namespace Serveo.Infrastructure.Authentication.Permissions
{
    public sealed class PermissionService(IUnitOfWork unitOfWork)
        : IPermissionService
    {
        public Task<bool> HasPermissionAsync(
            Guid userId,
            Guid tenantId,
            string permission,
            CancellationToken cancellationToken = default)
        {
            return unitOfWork.TenantMembers.Query()
                .Where(x =>
                    x.UserId == userId &&
                    x.TenantId == tenantId)
                .SelectMany(x => x.Role.RolePermissions)
                .AnyAsync(
                    x => x.Permission.Code == permission,
                    cancellationToken);
        }
    }
}
