using Serveo.Domain.Entities.Catalog;

namespace Serveo.Application.Abstractions.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<List<Category>> GetCategoriesFromMenuAsync(Guid menuId, bool includeProducts = false, CancellationToken cancellationToken = default);
    }
}
