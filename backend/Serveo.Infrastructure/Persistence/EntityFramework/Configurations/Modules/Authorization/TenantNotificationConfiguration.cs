using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Serveo.Domain.Entities.Authorization;

namespace Serveo.Infrastructure.Persistence.EntityFramework.Configurations.Modules.Authorization
{
    internal sealed class TenantNotificationConfiguration : IEntityTypeConfiguration<TenantNotification>
    {
        public void Configure(EntityTypeBuilder<TenantNotification> builder)
        {
            builder.Property(u => u.NotificationName).HasMaxLength(128);
            builder.Property(u => u.DataTypeName).HasMaxLength(256);
            builder.Property(u => u.EntityTypeName).HasMaxLength(256);
            builder.Property(u => u.EntityTypeAssemblyQualifiedName).HasMaxLength(512);
            builder.Property(u => u.EntityId).HasMaxLength(128);
        }
    }
}
