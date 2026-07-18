using Serveo.Domain.Entities.Identity;

namespace Serveo.Application.Abstractions.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> FindByEmailAsync(string email, CancellationToken ct = default);
        Task<bool> IsDuplicateEmailAsync(string email, Guid? id = null, CancellationToken ct = default);
        Task<bool> IsDuplicateUserNameAsync(string userName, Guid? id = null, CancellationToken ct = default);
    }
}
