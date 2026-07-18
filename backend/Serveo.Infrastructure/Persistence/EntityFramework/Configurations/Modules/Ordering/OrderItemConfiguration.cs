using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Serveo.Domain.Entities.Base;
using Serveo.Domain.Entities.Ordering;

namespace Serveo.Infrastructure.Persistence.EntityFramework.Configurations.Modules.Ordering
{
    internal sealed class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.Property(u => u.ProductName).HasMaxLength(128);
            builder.Property(u => u.Notes).HasMaxLength(512);
            builder.Property(u => u.DiscountCode).HasMaxLength(128);
            builder.Property(u => u.UnitPrice).HasColumnType(DbDecimals.Money);
            builder.Property(u => u.Quantity).HasColumnType(DbDecimals.Quantity);
            builder.Property(u => u.DiscountAmount).HasColumnType(DbDecimals.Money);
            builder.Property(u => u.FeeAmount).HasColumnType(DbDecimals.Money);
            builder.Property(u => u.FinalPrice).HasColumnType(DbDecimals.Money);
            builder.Property(u => u.DiscountValue).HasColumnType(DbDecimals.Money);

            builder.HasOne(x => x.Order)
                   .WithMany(o => o.Items)
                   .HasForeignKey(x => x.OrderId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Product)
                .WithMany()
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
