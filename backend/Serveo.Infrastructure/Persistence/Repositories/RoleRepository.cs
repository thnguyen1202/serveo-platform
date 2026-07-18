using Serveo.Application.Abstractions.Repositories;
using Serveo.Domain.Entities.Identity;
using Serveo.Infrastructure.Persistence.EntityFramework;

namespace Serveo.Infrastructure.Persistence.Repositories
{
    public sealed class RoleRepository(ApplicationDbContext db) : EfRepository<Role>(db), IRoleRepository
    {

    }
}
