using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Serveo.Domain.Entities.Catalog;

namespace Serveo.Infrastructure.Persistence.EntityFramework.Configurations.Modules.Catalog
{
    internal sealed class MenuConfiguration : IEntityTypeConfiguration<Menu>
    {
        public void Configure(EntityTypeBuilder<Menu> builder)
        {
            builder.Property(u => u.Name).HasMaxLength(128);
            builder.Property(u => u.Description).HasMaxLength(512);
        }
    }
}
