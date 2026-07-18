using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Serveo.Domain.Entities.Catalog;

namespace Serveo.Infrastructure.Persistence.EntityFramework.Configurations.Modules.Catalog
{
    internal sealed class CategoryTranslationConfiguration : IEntityTypeConfiguration<CategoryTranslation>
    {
        public void Configure(EntityTypeBuilder<CategoryTranslation> builder)
        {
            builder.Property(u => u.LanguageCode).HasMaxLength(8);
            builder.Property(u => u.Name).HasMaxLength(128);

            builder.HasKey(x => new
            {
                x.CategoryId,
                x.LanguageCode
            });
        }
    }
}
