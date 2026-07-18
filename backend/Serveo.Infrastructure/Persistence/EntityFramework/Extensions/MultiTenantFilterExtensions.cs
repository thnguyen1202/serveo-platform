namespace Serveo.Infrastructure.Persistence.EntityFramework.Extensions
{
    internal static class MultiTenantFilterExtensions
    {
        /// <summary>
        /// Apply multi-tenant query filter for all entities implementing IMayHaveTenant.
        /// Supports runtime TenantId and IgnoreQueryFilters property in DbContext.
        /// </summary>
        //public static void ApplyMultiTenantFilter(this ModelBuilder modelBuilder, EfDbContext dbContext)
        //{
        //    foreach (var entityType in modelBuilder.Model.GetEntityTypes()
        //         .Where(e => typeof(IMayHaveTenant).IsAssignableFrom(e.ClrType)))
        //    {
        //        var param = Expression.Parameter(entityType.ClrType, "e");

        //        // e.TenantId
        //        var tenantProp = Expression.Property(
        //            Expression.Convert(param, typeof(IMayHaveTenant)),
        //            nameof(IMayHaveTenant.TenantId));

        //        // dbContext.TenantId
        //        var dbContextTenant = Expression.Property(
        //            Expression.Constant(dbContext),
        //            nameof(EfDbContext.TenantId));

        //        // dbContext.TenantId == null
        //        var tenantIsNull =
        //            Expression.Equal(
        //                dbContextTenant,
        //                Expression.Constant(null, typeof(int?)));

        //        // e.TenantId == dbContext.TenantId
        //        var tenantEquals =
        //            Expression.Equal(tenantProp, dbContextTenant);

        //        // (@TenantId IS NULL OR TenantId = @TenantId)
        //        var body =
        //            Expression.OrElse(
        //                tenantIsNull,
        //                tenantEquals);

        //        var lambda = Expression.Lambda(body, param);

        //        entityType.SetQueryFilter(lambda);
        //    }
        //}
    }
}
