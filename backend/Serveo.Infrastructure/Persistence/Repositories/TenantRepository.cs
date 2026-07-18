using Serveo.Application.Abstractions.Repositories;
using Serveo.Domain.Entities.Tenanting;
using Serveo.Infrastructure.Persistence.EntityFramework;

namespace Serveo.Infrastructure.Persistence.Repositories
{
    public sealed class TenantRepository(ApplicationDbContext db) : EfRepository<Tenant>(db), ITenantRepository
    {

    }
}
