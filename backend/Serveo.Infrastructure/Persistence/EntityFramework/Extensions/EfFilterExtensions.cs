using Microsoft.EntityFrameworkCore;
using Serveo.Domain.Entities.Base;
using System.Linq.Expressions;

namespace Serveo.Infrastructure.Persistence.EntityFramework.Extensions
{
    internal static class EfFilterExtensions
    {
        //public static void ApplyGlobalFilters(this ModelBuilder builder, EfDbContext dbContext)
        //{
        //    foreach (var entityType in builder.Model.GetEntityTypes())
        //    {
        //        var clrType = entityType.ClrType;

        //        // SoftDelete filter
        //        if (typeof(ISoftDelete).IsAssignableFrom(clrType))
        //        {
        //            SetSoftDeleteFilter(builder, clrType);
        //        }

        //        // Tenant filter
        //        if (typeof(IMayHaveTenant).IsAssignableFrom(clrType))
        //        {
        //            SetTenantFilter(builder, clrType, dbContext);
        //        }
        //    }
        //}

        private static void SetSoftDeleteFilter(ModelBuilder builder, Type type)
        {
            var parameter = Expression.Parameter(type, "e");
            var prop = Expression.Property(Expression.Convert(parameter, typeof(ISoftDelete)), nameof(ISoftDelete.IsDeleted));
            var body = Expression.Equal(prop, Expression.Constant(false));
            var lambda = Expression.Lambda(body, parameter);

            builder.Entity(type).HasQueryFilter(lambda);
        }

        //private static void SetTenantFilter(ModelBuilder builder, Type type, EfDbContext dbContext)
        //{
        //    var parameter = Expression.Parameter(type, "e");

        //    var tenantProp = Expression.Property(
        //        Expression.Convert(parameter, typeof(IMayHaveTenant)),
        //        nameof(IMayHaveTenant.TenantId));

        //    var tenantIdProp = Expression.Property(
        //        Expression.Constant(dbContext),
        //        nameof(EfDbContext.TenantId));

        //    // (@TenantId IS NULL OR TenantId = @TenantId)
        //    var body = Expression.OrElse(
        //        Expression.Equal(tenantIdProp, Expression.Constant(null, typeof(int?))),
        //        Expression.Equal(tenantProp, tenantIdProp));

        //    var lambda = Expression.Lambda(body, parameter);

        //    builder.Entity(type).HasQueryFilter(lambda);
        //}
    }
}
