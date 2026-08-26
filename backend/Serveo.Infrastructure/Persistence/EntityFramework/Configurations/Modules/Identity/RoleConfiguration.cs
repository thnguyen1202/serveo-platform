using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Serveo.Domain.Entities.Identity;

namespace Serveo.Infrastructure.Persistence.EntityFramework.Configurations.Modules.Identity
{
    internal sealed class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Roles");
            builder.Property(u => u.Code).HasMaxLength(128);
            builder.Property(u => u.ConcurrencyStamp).HasMaxLength(128);
            builder.Property(u => u.Description).HasMaxLength(512);

            builder.HasIndex(x => new
            {
                x.TenantId,
                x.Code
            })
            .IsUnique();
        }
    }
}
