using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Serveo.Domain.Entities.Tenanting;

namespace Serveo.Infrastructure.Persistence.EntityFramework.Configurations.Modules.Tenanting
{
    internal sealed class BranchConfiguration : IEntityTypeConfiguration<Branch>
    {
        public void Configure(EntityTypeBuilder<Branch> builder)
        {
            builder.Property(u => u.Name).HasMaxLength(256);
            builder.Property(u => u.Address).HasMaxLength(256);
        }
    }
}
