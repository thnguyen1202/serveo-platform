using Serveo.Application.Abstractions.Repositories;
using Serveo.Domain.Entities.Authorization;
using Serveo.Infrastructure.Persistence.EntityFramework;

namespace Serveo.Infrastructure.Persistence.Repositories
{
    public sealed class RolePermissionRepository(ApplicationDbContext db) : EfRepository<RolePermission>(db), IRolePermissionRepository
    {

    }
}
