using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Serveo.Domain.Entities.Catalog;

namespace Serveo.Infrastructure.Persistence.EntityFramework.Configurations.Modules.Catalog
{
    internal sealed class MenuTranslationConfiguration : IEntityTypeConfiguration<MenuTranslation>
    {
        public void Configure(EntityTypeBuilder<MenuTranslation> builder)
        {
            builder.Property(u => u.LanguageCode).HasMaxLength(8);
            builder.Property(u => u.Name).HasMaxLength(128);
            builder.Property(u => u.Description).HasMaxLength(512);

            builder.HasKey(x => new
            {
                x.MenuId,
                x.LanguageCode
            });
        }
    }
}
