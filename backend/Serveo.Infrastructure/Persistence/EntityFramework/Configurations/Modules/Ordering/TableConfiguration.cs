using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Serveo.Domain.Entities.Ordering;

namespace Serveo.Infrastructure.Persistence.EntityFramework.Configurations.Modules.Ordering
{
    internal sealed class TableConfiguration : IEntityTypeConfiguration<Table>
    {
        public void Configure(EntityTypeBuilder<Table> builder)
        {
            builder.Property(x => x.Code).HasMaxLength(64);
            builder.Property(x => x.Name).HasMaxLength(128);
            builder.Property(x => x.PublicToken).HasMaxLength(128);
        }
    }
}
