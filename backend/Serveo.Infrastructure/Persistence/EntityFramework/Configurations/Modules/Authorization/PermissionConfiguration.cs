using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Serveo.Domain.Entities.Authorization;

namespace Serveo.Infrastructure.Persistence.EntityFramework.Configurations.Modules.Authorization
{
    internal sealed class PermissionConfiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.Property(u => u.Code).HasMaxLength(128);
            builder.Property(u => u.Name).HasMaxLength(128);
            builder.Property(u => u.Description).HasMaxLength(512);
            builder.Property(u => u.Module).HasMaxLength(128);

            builder.HasIndex(x => x.Code).IsUnique();
        }
    }
}
