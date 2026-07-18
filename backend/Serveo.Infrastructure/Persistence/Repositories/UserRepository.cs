using Microsoft.EntityFrameworkCore;
using Serveo.Application.Abstractions.Repositories;
using Serveo.Domain.Entities.Identity;
using Serveo.Infrastructure.Persistence.EntityFramework;

namespace Serveo.Infrastructure.Persistence.Repositories
{
    public sealed class UserRepository(ApplicationDbContext db) : EfRepository<User>(db), IUserRepository
    {
        public Task<User?> FindByEmailAsync(string email, CancellationToken ct = default)
        {
            return FindAsync(x => x.Email == email, ct);
        }

        public async Task<bool> IsDuplicateEmailAsync(string email, Guid? id = null, CancellationToken ct = default)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            return await DbSet
                .AnyAsync(x =>
                    x.NormalizedEmail == email.ToUpperInvariant().Trim()
                    && (!id.HasValue || x.Id != id.Value), ct);
        }

        public async Task<bool> IsDuplicateUserNameAsync(string userName, Guid? id = null, CancellationToken ct = default)
        {
            if (string.IsNullOrWhiteSpace(userName))
                return false;

            return await DbSet
                .AnyAsync(x =>
                    x.NormalizedUserName == userName.ToUpperInvariant().Trim()
                    && (!id.HasValue || x.Id != id.Value), ct);
        }
    }
}
