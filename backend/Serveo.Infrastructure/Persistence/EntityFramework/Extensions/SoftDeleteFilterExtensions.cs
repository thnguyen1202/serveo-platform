using Microsoft.EntityFrameworkCore;
using Serveo.Domain.Entities.Base;
using System.Linq.Expressions;

namespace Serveo.Infrastructure.Persistence.EntityFrameworkCore.Extensions
{
    internal static class SoftDeleteFilterExtensions
    {
        public static void ApplySoftDeleteFilter(this ModelBuilder builder)
        {
            foreach (var entityType in builder.Model.GetEntityTypes().Where(e => typeof(ISoftDelete).IsAssignableFrom(e.ClrType)))
            {
                var parameter = Expression.Parameter(entityType.ClrType, "e");

                var prop = Expression.Property(parameter, nameof(ISoftDelete.IsDeleted));

                var body = Expression.Equal(prop, Expression.Constant(false));

                var lambda = Expression.Lambda(body, parameter);

                builder.Entity(entityType.ClrType).HasQueryFilter(lambda);
            }
        }
    }
}
