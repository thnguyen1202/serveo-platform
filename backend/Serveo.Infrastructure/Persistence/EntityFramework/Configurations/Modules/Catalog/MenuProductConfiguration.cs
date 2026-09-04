using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Serveo.Domain.Entities.Base;
using Serveo.Domain.Entities.Catalog;

namespace Serveo.Infrastructure.Persistence.EntityFramework.Configurations.Modules.Catalog
{
    internal sealed class MenuProductConfiguration : IEntityTypeConfiguration<MenuProduct>
    {
        public void Configure(EntityTypeBuilder<MenuProduct> builder)
        {
            //builder.HasKey(r => new { r.MenuId, r.ProductId });
            //builder.HasIndex(x => x.ProductId);

            builder.HasIndex(u => new { u.MenuId, u.ProductId })
                .IsUnique();

            builder.Property(u => u.PriceOverride).HasColumnType(DbDecimals.Money);

            

            builder.HasOne(x => x.Menu)
                .WithMany(x => x.MenuProducts)
                .HasForeignKey(x => x.MenuId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Product)
                .WithMany(x => x.MenuProducts)
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
