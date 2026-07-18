using Serveo.Application.Abstractions.Repositories;
using Serveo.Domain.Entities.Catalog;
using Serveo.Infrastructure.Persistence.EntityFramework;

namespace Serveo.Infrastructure.Persistence.Repositories
{
    public sealed class ProductRepository(ApplicationDbContext db) : EfRepository<Product>(db), IProductRepository
    {

    }
}
