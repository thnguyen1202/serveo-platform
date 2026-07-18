using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Serveo.Domain.Entities.Catalog;

namespace Serveo.Infrastructure.Persistence.EntityFramework.Configurations.Modules.Catalog
{
    internal sealed class MenuItemConfiguration : IEntityTypeConfiguration<MenuItem>
    {
        public void Configure(EntityTypeBuilder<MenuItem> builder)
        {
            builder.HasKey(r => new { r.MenuId, r.ProductId });
            //builder.HasIndex(x => new { x.MenuId, x.ProductId }).IsUnique();

            builder.HasIndex(x => x.ProductId);

            builder.HasOne(x => x.Menu)
                .WithMany(x => x.Items)
                .HasForeignKey(x => x.MenuId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Product)
                .WithMany(x => x.MenuItems)
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
