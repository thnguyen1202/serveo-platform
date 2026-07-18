using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Serveo.Domain.Entities.Tenanting;

namespace Serveo.Infrastructure.Persistence.EntityFramework.Configurations.Modules.Tenanting
{
    internal sealed class BusinessConfiguration : IEntityTypeConfiguration<Business>
    {
        public void Configure(EntityTypeBuilder<Business> builder)
        {
            builder.Property(u => u.Name).HasMaxLength(256);
            builder.Property(u => u.Phone).HasMaxLength(32);
            builder.Property(u => u.Address).HasMaxLength(256);
        }
    }
}
