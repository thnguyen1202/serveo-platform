using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Serveo.Domain.Entities.Authorization;

namespace Serveo.Infrastructure.Persistence.EntityFramework.Configurations.Modules.Authorization
{
    internal sealed class ApiClientKeyConfiguration : IEntityTypeConfiguration<ApiClientKey>
    {
        public void Configure(EntityTypeBuilder<ApiClientKey> builder)
        {
            builder.Property(u => u.KeyHash).HasMaxLength(1024);
            builder.Property(u => u.LastUsedIp).HasMaxLength(64);

            builder.HasIndex(p => p.KeyHash).IsUnique();

            builder.HasOne(x => x.Client)
                .WithMany(x => x.ClientKeys)
                .HasForeignKey(x => x.ClientId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
