using Microsoft.EntityFrameworkCore;

namespace Serveo.Infrastructure.Persistence.EntityFramework.Extensions
{
    internal static class EfValidationExtensions
    {
        public static void ValidateBeforeSave(this DbContext context)
        {
            var errors = new List<string>();

            foreach (var entry in context.ChangeTracker.Entries())
            {
                if (entry.State is EntityState.Unchanged or EntityState.Detached)
                    continue;

                foreach (var prop in entry.Properties)
                {
                    var value = prop.CurrentValue;

                    // =========================
                    // 1. STRING MAX LENGTH
                    // =========================
                    var maxLength = prop.Metadata.GetMaxLength();

                    if (maxLength is not null &&
                        value is string s &&
                        s.Length > maxLength)
                    {
                        errors.Add(
                            $"[STRING] {entry.Entity.GetType().Name}.{prop.Metadata.Name} " +
                            $"MaxLength={maxLength}, Actual={s.Length}");
                    }

                    // =========================
                    // 2. DECIMAL PRECISION / SCALE
                    // =========================
                    if (value is decimal decimalValue)
                    {
                        var precision = prop.Metadata.GetPrecision();
                        var scale = prop.Metadata.GetScale();

                        if (precision is not null && scale is not null)
                        {
                            var parts = decimalValue.ToString(System.Globalization.CultureInfo.InvariantCulture)
                                .Split('.');

                            var integerPartLength = parts[0].Replace("-", "").Length;
                            var scaleLength = parts.Length > 1 ? parts[1].Length : 0;

                            var totalDigits = integerPartLength + scaleLength;

                            if (totalDigits > precision || scaleLength > scale)
                            {
                                errors.Add(
                                    $"[DECIMAL] {entry.Entity.GetType().Name}.{prop.Metadata.Name} " +
                                    $"Precision={precision}, Scale={scale}, " +
                                    $"Value={decimalValue}");
                            }
                        }
                    }

                    // =========================
                    // 3. Nullable check
                    // =========================
                    if (!prop.Metadata.IsNullable && value is null)
                    {
                        errors.Add($"[NULL] {prop.Metadata.Name} is required");
                    }

                    // =========================
                    // 4. Enum overflow (optional)
                    // =========================
                    if (value is int intValue &&
                        prop.Metadata.ClrType.IsEnum &&
                        !Enum.IsDefined(prop.Metadata.ClrType, intValue))
                    {
                        errors.Add($"[ENUM] Invalid value {intValue}");
                    }
                }
            }

            if (errors.Count > 0)
            {
                throw new InvalidOperationException(
                    "Validation failed before SaveChanges:\n" +
                    string.Join("\n", errors));
            }
        }
    }
}
