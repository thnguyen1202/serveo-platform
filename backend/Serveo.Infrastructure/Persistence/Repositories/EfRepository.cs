using Microsoft.EntityFrameworkCore;
using Serveo.Application.Abstractions.Repositories;
using Serveo.Infrastructure.Persistence.EntityFramework;
using System.Linq.Expressions;

namespace Serveo.Infrastructure.Persistence.Repositories
{
    public class EfRepository<TEntity>(ApplicationDbContext db) : IRepository<TEntity> where TEntity : class
    {
        protected readonly ApplicationDbContext DbContext = db;
        protected DbSet<TEntity> DbSet => DbContext.Set<TEntity>();

        public IQueryable<TEntity> Query(bool asNoTracking = true)
            => asNoTracking ? DbSet.AsNoTracking() : DbSet;

        public Task<TEntity?> FindAsync(object[] keys, CancellationToken ct = default)
            => DbSet.FindAsync(keys, ct).AsTask();

        public Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken ct = default)
            => DbSet.FirstOrDefaultAsync(predicate, ct);

        public Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken ct = default)
            => DbSet.AnyAsync(predicate, ct);

        public void Add(TEntity entity) => DbSet.Add(entity);
        public void AddRange(IEnumerable<TEntity> entities) => DbSet.AddRange(entities);
        public void Update(TEntity entity) => DbSet.Update(entity);
        public void Remove(TEntity entity) => DbSet.Remove(entity);
        public void RemoveRange(IEnumerable<TEntity> entities) => DbSet.RemoveRange(entities);
    }
}
