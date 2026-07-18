using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Serveo.Domain.Entities.Base;
using Serveo.Domain.Entities.Identity;

namespace Serveo.Infrastructure.Persistence.EntityFramework.Configurations.Modules.Identity
{
    internal sealed class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.Property(x => x.TokenHash).HasMaxLength(DbLengths.NormalText);
            builder.Property(x => x.CreatedByIp).HasMaxLength(DbLengths.Name);
            builder.Property(x => x.RevokedByIp).HasMaxLength(DbLengths.Name);
            builder.Property(x => x.ReplacedByTokenHash).HasMaxLength(DbLengths.NormalText);
        }
    }
}
