using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace Serveo.Infrastructure.Persistence.EntityFramework.Extensions
{
    internal static partial class SnakeCaseNamingExtensions
    {
        public static void ApplySnakeCaseNamingConvention(this ModelBuilder modelBuilder)
        {
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                // table name
                var tableName = entity.GetTableName();
                if (tableName != null)
                    entity.SetTableName(ToSnakeCase(tableName));

                // column name
                foreach (var property in entity.GetProperties())
                {
                    var columnName = property.GetColumnName();
                    if (columnName != null)
                        property.SetColumnName(ToSnakeCase(columnName));
                }

                // key name
                foreach (var key in entity.GetKeys())
                {
                    var keyName = key.GetName();
                    if (keyName != null)
                        key.SetName(ToSnakeCase(keyName));
                }

                // foreign key name
                foreach (var foreignKey in entity.GetForeignKeys())
                {
                    var foreignKeyName = foreignKey.GetConstraintName();
                    if (foreignKeyName != null)
                        foreignKey.SetConstraintName(ToSnakeCase(foreignKeyName));
                }

                // index name
                foreach (var index in entity.GetIndexes())
                {
                    var indexName = index.GetDatabaseName();
                    if (indexName != null)
                        index.SetDatabaseName(ToSnakeCase(indexName));
                }
            }
        }

        [GeneratedRegex("([a-z0-9])([A-Z])")]
        private static partial Regex LowerUpperRegex();

        [GeneratedRegex("([A-Z]+)([A-Z][a-z])")]
        private static partial Regex AcronymRegex();

        private static string ToSnakeCase(string input)
        {
            input = LowerUpperRegex().Replace(input, "$1_$2");
            input = AcronymRegex().Replace(input, "$1_$2");

            return input.ToLowerInvariant();
        }
    }
}
