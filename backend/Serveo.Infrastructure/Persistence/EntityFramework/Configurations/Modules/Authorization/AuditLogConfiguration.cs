using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Serveo.Domain.Entities.Authorization;

namespace Serveo.Infrastructure.Persistence.EntityFramework.Configurations.Modules.Authorization
{
    internal sealed class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
    {
        public void Configure(EntityTypeBuilder<AuditLog> builder)
        {
            builder.Property(u => u.TableName).HasMaxLength(128);
            builder.Property(u => u.Action).HasMaxLength(32);
            builder.Property(u => u.KeyValues).HasMaxLength(256);
            builder.Property(u => u.ChangedColumns).HasMaxLength(1024);
        }
    }
}
