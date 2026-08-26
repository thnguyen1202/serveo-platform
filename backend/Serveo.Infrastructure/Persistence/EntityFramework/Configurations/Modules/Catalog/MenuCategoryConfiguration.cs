using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Serveo.Domain.Entities.Base;
using Serveo.Domain.Entities.Catalog;

namespace Serveo.Infrastructure.Persistence.EntityFramework.Configurations.Modules.Catalog
{
    internal sealed class MenuCategoryConfiguration : IEntityTypeConfiguration<MenuCategory>
    {
        public void Configure(EntityTypeBuilder<MenuCategory> builder)
        {
            builder.HasKey(r => new { r.MenuId, r.CategoryId });

            builder.HasIndex(x => x.CategoryId);

            builder.HasOne(x => x.Menu)
                .WithMany(x => x.MenuCategories)
                .HasForeignKey(x => x.MenuId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Category)
                .WithMany(x => x.MenuCategories)
                .HasForeignKey(x => x.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
