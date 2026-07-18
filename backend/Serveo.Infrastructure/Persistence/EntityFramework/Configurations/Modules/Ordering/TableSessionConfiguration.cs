using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Serveo.Domain.Entities.Base;
using Serveo.Domain.Entities.Ordering;

namespace Serveo.Infrastructure.Persistence.EntityFramework.Configurations.Modules.Ordering
{
    internal sealed class TableSessionConfiguration : IEntityTypeConfiguration<TableSession>
    {
        public void Configure(EntityTypeBuilder<TableSession> builder)
        {
            builder.Property(x => x.SessionCode).HasMaxLength(32);
            builder.Property(x => x.Notes).HasMaxLength(512);
            builder.Property(x => x.TotalAmount).HasColumnType(DbDecimals.Money);
            builder.Property(x => x.PaidAmount).HasColumnType(DbDecimals.Money);
        }
    }
}
