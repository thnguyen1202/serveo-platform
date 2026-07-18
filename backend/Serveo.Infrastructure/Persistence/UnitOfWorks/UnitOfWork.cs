using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Serveo.Application.Abstractions;
using Serveo.Application.Abstractions.Repositories;
using Serveo.Domain.Interfaces;
using Serveo.Infrastructure.Persistence.EntityFramework;
using Serveo.Infrastructure.Persistence.Repositories;

namespace Serveo.Infrastructure.Persistence.UnitOfWorks
{
    public class UnitOfWork<TDbContext>(TDbContext dbContext, ISessionContext session) : IUnitOfWork where TDbContext : ApplicationDbContext
    {
        private readonly TDbContext _dbContext = dbContext;
        private IDbContextTransaction? _transaction;
        //private readonly Dictionary<Type, object> _repositories = [];

        public ISessionContext Session { get; } = session;

        public IUserRepository Users => new UserRepository(_dbContext);
        public IRoleRepository Roles => new RoleRepository(_dbContext);

        public ITenantRepository Tenants => new TenantRepository(_dbContext);
        public IBusinessRepository Businesses => new BusinessRepository(_dbContext);
        public IBranchRepository Branches => new BranchRepository(_dbContext);

        public ICategoryRepository Categories => new CategoryRepository(_dbContext);
        public IMenuRepository Menus => new MenuRepository(_dbContext);
        public IProductRepository Products => new ProductRepository(_dbContext);

        public ITableRepository Tables => new TableRepository(_dbContext);
        public IOrderRepository Orders => new OrderRepository(_dbContext);


        public DbSet<TEntity> Set<TEntity>() where TEntity : class => _dbContext.Set<TEntity>();

        public void SetValues<TEntity>(TEntity original, object current) where TEntity : class
        {
            var entry = _dbContext.Entry(original);

            if (entry.State == EntityState.Detached)
            {
                _dbContext.Attach(original);
            }

            entry.CurrentValues.SetValues(current);
        }

        public void Add<TEntity>(TEntity entity) where TEntity : class
        {
            _dbContext.Set<TEntity>().Add(entity);
        }
        public void Update<TEntity>(TEntity entity) where TEntity : class
        {
            _dbContext.Set<TEntity>().Update(entity);
        }
        public void Remove<TEntity>(TEntity entity) where TEntity : class
        {
            _dbContext.Set<TEntity>().Remove(entity);
        }


        public async Task<int> SaveChangesAsync(CancellationToken ct = default) => await _dbContext.SaveChangesAsync(ct);


        // Transaction management
        public async Task BeginTransactionAsync(CancellationToken ct = default)
        {
            _transaction ??= await _dbContext.Database.BeginTransactionAsync(ct);
        }

        public async Task CommitAsync(CancellationToken ct = default)
        {
            if (_transaction != null)
            {
                await _dbContext.SaveChangesAsync(ct);
                await _transaction.CommitAsync(ct);
                await _transaction.DisposeAsync();
                _transaction = null;
            }
            else
            {
                await _dbContext.SaveChangesAsync(ct);
                return;
            }
        }

        public async Task RollbackAsync(CancellationToken ct = default)
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync(ct);
                await _transaction.DisposeAsync();
                _transaction = null;

                // optional: clear tracked entities
                _dbContext.ChangeTracker.Clear();
            }
        }

        public async Task ExecuteAsync(Func<Task> action, CancellationToken ct = default)
        {
            await BeginTransactionAsync(ct);

            try
            {
                await action();
                await CommitAsync(ct);
            }
            catch
            {
                await RollbackAsync(ct);
                throw;
            }
        }

        public async Task<T> ExecuteAsync<T>(Func<Task<T>> action, CancellationToken ct = default)
        {
            await BeginTransactionAsync(ct);

            try
            {
                T result = await action();
                await CommitAsync(ct);
                return result;
            }
            catch
            {
                await RollbackAsync(ct);
                throw;
            }
        }

        #region Dispose
        private bool disposedValue;

        // Dispose only transaction; DbContext managed by DI
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _transaction?.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
