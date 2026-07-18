using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Serveo.Infrastructure.Persistence.Interceptors
{
    public sealed class TrimStringsInterceptor : SaveChangesInterceptor
    {
        public override ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken cancellationToken = default)
        {
            TrimStrings(eventData.Context);
            return base.SavedChangesAsync(eventData, result, cancellationToken);
        }

        public override int SavedChanges(SaveChangesCompletedEventData eventData, int result)
        {
            TrimStrings(eventData.Context);
            return base.SavedChanges(eventData, result);
        }

        private static void TrimStrings(DbContext? context)
        {
            if (context == null) return;

            foreach (var entry in context.ChangeTracker.Entries())
            {
                if (entry.State != EntityState.Added && entry.State != EntityState.Modified)
                    continue;

                foreach (var prop in entry.Properties)
                {
                    if (prop.Metadata.ClrType == typeof(string) && prop.CurrentValue is string s)
                    {
                        var trimmed = s.Trim();
                        if (trimmed != s)
                            prop.CurrentValue = trimmed;
                    }
                }
            }
        }
    }
}
