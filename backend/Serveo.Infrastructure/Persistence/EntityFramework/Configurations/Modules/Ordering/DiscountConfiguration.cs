using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Serveo.Domain.Entities.Base;
using Serveo.Domain.Entities.Ordering;

namespace Serveo.Infrastructure.Persistence.EntityFramework.Configurations.Modules.Ordering
{
    internal sealed class DiscountConfiguration : IEntityTypeConfiguration<Discount>
    {
        public void Configure(EntityTypeBuilder<Discount> builder)
        {
            builder.Property(u => u.Code).HasMaxLength(64);
            builder.Property(u => u.Value).HasColumnType(DbDecimals.Money);
            builder.Property(u => u.MaxDiscountAmount).HasColumnType(DbDecimals.Money);
        }
    }
}
