using Serveo.Application.Abstractions.Repositories;
using Serveo.Domain.Entities.Ordering;
using Serveo.Infrastructure.Persistence.EntityFramework;

namespace Serveo.Infrastructure.Persistence.Repositories
{
    public sealed class TableRepository(ApplicationDbContext db) : EfRepository<Table>(db), ITableRepository
    {

    }
}
