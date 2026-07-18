using Serveo.Application.Abstractions.Repositories;
using Serveo.Domain.Entities.Ordering;
using Serveo.Infrastructure.Persistence.EntityFramework;

namespace Serveo.Infrastructure.Persistence.Repositories
{
    public sealed class OrderRepository(ApplicationDbContext db) : EfRepository<Order>(db), IOrderRepository
    {

    }
}
