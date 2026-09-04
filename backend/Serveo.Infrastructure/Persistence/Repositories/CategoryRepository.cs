using Microsoft.EntityFrameworkCore;
using Serveo.Application.Abstractions.Repositories;
using Serveo.Domain.Entities.Catalog;
using Serveo.Infrastructure.Persistence.EntityFramework;

namespace Serveo.Infrastructure.Persistence.Repositories
{
    public sealed class CategoryRepository(ApplicationDbContext db) : EfRepository<Category>(db), ICategoryRepository
    {
        public async Task<List<Category>> GetCategoriesFromMenuAsync(
            Guid menuId,
            bool includeProducts = false,
            CancellationToken cancellationToken = default)
        {
            IQueryable<Category> query = DbContext.Set<Category>()
                .AsNoTracking()
                .Where(c => c.MenuCategories.Any(mc => mc.MenuId == menuId));

            if (includeProducts)
            {
                query = query.Include(c => c.Products);
            }

            return await query.ToListAsync(cancellationToken);
        }
    }
}
