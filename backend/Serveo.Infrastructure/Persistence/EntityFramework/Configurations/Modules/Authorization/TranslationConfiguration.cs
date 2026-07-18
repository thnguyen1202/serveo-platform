using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Serveo.Domain.Entities.Authorization;

namespace Serveo.Infrastructure.Persistence.EntityFramework.Configurations.Modules.Authorization
{
    internal sealed class TranslationConfiguration : IEntityTypeConfiguration<Translation>
    {
        public void Configure(EntityTypeBuilder<Translation> builder)
        {
            builder.Property(u => u.EntityName).HasMaxLength(64);
            builder.Property(u => u.PropertyName).HasMaxLength(64);
            builder.Property(u => u.EntityId).HasMaxLength(128);
        }
    }
}
