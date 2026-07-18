using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Serveo.Domain.Entities.Tenanting;

namespace Serveo.Infrastructure.Persistence.EntityFramework.Configurations.Modules.Tenanting
{
    internal sealed class TenantConfiguration : IEntityTypeConfiguration<Tenant>
    {
        public void Configure(EntityTypeBuilder<Tenant> builder)
        {
            builder.Property(u => u.Code).HasMaxLength(128);
            builder.Property(u => u.Name).HasMaxLength(128);
        }
    }
}
