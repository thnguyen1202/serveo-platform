using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Serveo.Domain.Entities.Base;
using Serveo.Domain.Entities.Ordering;

namespace Serveo.Infrastructure.Persistence.EntityFramework.Configurations.Modules.Ordering
{
    internal sealed class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(u => u.SubTotal).HasColumnType(DbDecimals.Money);
            builder.Property(u => u.DiscountAmount).HasColumnType(DbDecimals.Money);
            builder.Property(u => u.FeeAmount).HasColumnType(DbDecimals.Money);
            builder.Property(u => u.TaxAmount).HasColumnType(DbDecimals.Money);
            builder.Property(u => u.TotalAmount).HasColumnType(DbDecimals.Money);
            builder.Property(u => u.DiscountCode).HasMaxLength(128);
            builder.Property(u => u.DiscountValue).HasColumnType(DbDecimals.Money);
            builder.Property(u => u.Notes).HasMaxLength(512);

            builder.HasOne(x => x.Branch)
                .WithMany()
                .HasForeignKey(x => x.BranchId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
