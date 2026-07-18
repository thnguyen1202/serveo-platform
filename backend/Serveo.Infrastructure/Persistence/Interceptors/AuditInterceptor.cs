using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Serveo.Domain.Entities.Authorization;
using Serveo.Domain.Entities.Base;
using Serveo.Domain.Interfaces;
using System.Runtime.CompilerServices;

namespace Serveo.Infrastructure.Persistence.Interceptors
{
    public sealed class AuditInterceptor(ISessionContext session) : SaveChangesInterceptor
    {
        private readonly ISessionContext _session = session ?? throw new ArgumentNullException(nameof(session));
        private static readonly ConditionalWeakTable<DbContext, List<AuditEntry>> _auditEntries = [];

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            var context = eventData.Context!;
            if (context == null)
                return base.SavingChangesAsync(eventData, result, cancellationToken);

            var entries = CreateAuditEntries(context);
            if (entries.Count > 0)
                _auditEntries.Add(context, entries);

            var now = DateTime.UtcNow;
            foreach (var entry in context.ChangeTracker.Entries())
            {
                if (entry.State == EntityState.Unchanged ||
                    entry.State == EntityState.Detached)
                    continue;

                var entity = entry.Entity;

                switch (entry.State)
                {
                    case EntityState.Added:
                        HandleAdded(entity, now);
                        break;

                    case EntityState.Modified:
                        HandleModified(entry, entity, now);
                        break;

                    case EntityState.Deleted:
                        HandleDeleted(entry, entity, now);
                        break;
                }
            }

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        public override async ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken cancellationToken = default)
        {
            var context = eventData.Context;
            if (context == null) return result;

            if (!_auditEntries.TryGetValue(context, out var auditEntries))
                return result;

            foreach (var auditEntry in auditEntries)
            {
                // Hoàn tất key nếu là temporary
                foreach (var prop in auditEntry.TemporaryProperties)
                {
                    if (prop.Metadata.IsPrimaryKey())
                        auditEntry.KeyValues[prop.Metadata.Name] = prop.CurrentValue;
                }

                // Thêm AuditLog vào DbSet, EF sẽ commit cùng transaction
                context.Set<AuditLog>().Add(
                    auditEntry.ToAuditLog(_session.TenantId, _session.UserId)
                );
            }

            _auditEntries.Remove(context);

            await context.SaveChangesAsync(cancellationToken);
            // Không gọi SaveChangesAsync lần 2
            // AuditLog sẽ được commit cùng SaveChanges gốc
            return result;
        }

        private static List<AuditEntry> CreateAuditEntries(DbContext context)
        {
            context.ChangeTracker.DetectChanges();

            var entries = new List<AuditEntry>();

            foreach (var entry in context.ChangeTracker.Entries())
            {
                if (entry.Entity is AuditLog ||
                    entry.State == EntityState.Detached ||
                    entry.State == EntityState.Unchanged)
                    continue;

                var auditEntry = new AuditEntry(entry);

                foreach (var property in entry.Properties)
                {
                    var name = property.Metadata.Name;

                    if (property.Metadata.IsPrimaryKey())
                    {
                        if (property.IsTemporary)
                            auditEntry.TemporaryProperties.Add(property);
                        else
                            auditEntry.KeyValues[name] = property.CurrentValue;
                        continue;
                    }

                    switch (entry.State)
                    {
                        case EntityState.Added:
                            auditEntry.NewValues[name] = property.CurrentValue;
                            break;

                        case EntityState.Deleted:
                            auditEntry.OldValues[name] = property.OriginalValue;
                            break;

                        case EntityState.Modified:
                            if (!Equals(property.OriginalValue, property.CurrentValue))
                            {
                                auditEntry.ChangedColumns.Add(name);
                                auditEntry.OldValues[name] = property.OriginalValue;
                                auditEntry.NewValues[name] = property.CurrentValue;
                            }
                            break;
                    }
                }

                entries.Add(auditEntry);
            }

            return entries;
        }

        //public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        //{
        //    var context = eventData.Context!;
        //    if (context == null)
        //        return base.SavingChanges(eventData, result);

        //    var now = DateTime.UtcNow;
        //    foreach (var entry in context.ChangeTracker.Entries())
        //    {
        //        if (entry.State == EntityState.Unchanged ||
        //            entry.State == EntityState.Detached)
        //            continue;

        //        var entity = entry.Entity;

        //        switch (entry.State)
        //        {
        //            case EntityState.Added:
        //                HandleAdded(entry, entity, now);
        //                break;

        //            case EntityState.Modified:
        //                HandleModified(entry, entity, now);
        //                break;

        //            case EntityState.Deleted:
        //                HandleDeleted(entry, entity, now);
        //                break;
        //        }
        //    }

        //    return base.SavingChanges(eventData, result);
        //}

        private void HandleAdded(object entity, DateTime now)
        {
            if (entity is IHasCreatedAt creation)
                creation.CreatedAt = now;

            //if (entity is ICreationAudited audited)
            //    audited.CreatedBy = _session.UserId;

            if (entity is IMayHaveTenant tenant)
                tenant.TenantId ??= _session.TenantId;
        }

        private static void HandleModified(EntityEntry entry, object entity, DateTime now)
        {
            if (entity is IHasModifiedAt modification)
                modification.ModifiedAt = now;

            //if (entity is IModificationAudited audited)
            //    audited.ModifiedBy = _session.UserId;

            // Block editing CreationTime
            if (entity is IHasCreatedAt)
                entry.Property(nameof(IHasCreatedAt.CreatedAt)).IsModified = false;

            // Block editing TenantId
            if (entity is IMayHaveTenant)
                entry.Property(nameof(IMayHaveTenant.TenantId)).IsModified = false;

            if (entity is IMustHaveTenant)
                entry.Property(nameof(IMustHaveTenant.TenantId)).IsModified = false;
        }

        private static void HandleDeleted(EntityEntry entry, object entity, DateTime now)
        {
            if (entity is ISoftDelete softDelete)
            {
                entry.State = EntityState.Modified;

                softDelete.IsDeleted = true;

                if (entity is IHasDeletedAt deletionTime)
                    deletionTime.DeletedAt = now;

                //if (entity is IDeletionAudited audited)
                //    audited.DeletedBy = _session.UserId;
            }
        }
    }
}
