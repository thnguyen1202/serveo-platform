using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Serveo.Domain.Entities.Base;
using System.Reflection;

namespace Serveo.Infrastructure.Persistence.EntityFrameworkCore.Extensions
{
    internal static class UtcDateTimeExtensions
    {
        private static readonly string[] DefaultUtcPropertyNames = ["CreationTime", "LastModificationTime", "DeletionTime"];

        /// <summary>
        /// Apply UTC DateTime conversion for all DateTime / DateTime? properties
        /// that are marked with [Utc] or match default UTC property names.
        /// </summary>
        public static void ApplyUtcDateTimeConversion(this ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var property in entityType.GetProperties())
                {
                    if (property.ClrType != typeof(DateTime) && property.ClrType != typeof(DateTime?))
                        continue;

                    var memberInfo = property.PropertyInfo ?? property.FieldInfo as MemberInfo;
                    if (memberInfo == null)
                        continue;

                    // check if property has [Utc] or default names
                    if (!Attribute.IsDefined(memberInfo, typeof(UtcAttribute)) &&
                        !DefaultUtcPropertyNames.Any(n => n.Equals(property.Name, StringComparison.OrdinalIgnoreCase)))
                        continue;

                    var converter = new ValueConverter<DateTime, DateTime>(
                        v => v.Kind == DateTimeKind.Utc ? v : v.ToUniversalTime(),
                        v => DateTime.SpecifyKind(v, DateTimeKind.Utc)
                    );

                    property.SetValueConverter(converter);
                }
            }
        }
    }
}
