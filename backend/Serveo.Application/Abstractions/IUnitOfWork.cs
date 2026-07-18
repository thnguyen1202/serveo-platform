using Microsoft.EntityFrameworkCore;
using Serveo.Application.Abstractions.Repositories;
using Serveo.Domain.Interfaces;

namespace Serveo.Application.Abstractions
{
    public interface IUnitOfWork : IDisposable
    {
        ISessionContext Session { get; }

        IUserRepository Users { get; }
        ITenantRepository Tenants { get; }
        IBusinessRepository Businesses { get; }
        IBranchRepository Branches { get; }
        IRoleRepository Roles { get; }
        ICategoryRepository Categories { get; }
        IMenuRepository Menus { get; }
        IProductRepository Products { get; }
        ITableRepository Tables { get; }
        IOrderRepository Orders { get; }

        void Add<TEntity>(TEntity entity) where TEntity : class;
        //Task BeginTransactionAsync(CancellationToken ct = default);
        //Task CommitAsync(CancellationToken ct = default);
        Task ExecuteAsync(Func<Task> action, CancellationToken ct = default);
        Task<T> ExecuteAsync<T>(Func<Task<T>> action, CancellationToken ct = default);
        void Remove<TEntity>(TEntity entity) where TEntity : class;
        //Task RollbackAsync(CancellationToken ct = default);
        Task<int> SaveChangesAsync(CancellationToken ct = default);
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        void SetValues<TEntity>(TEntity original, object current) where TEntity : class;
        void Update<TEntity>(TEntity entity) where TEntity : class;
    }
}
