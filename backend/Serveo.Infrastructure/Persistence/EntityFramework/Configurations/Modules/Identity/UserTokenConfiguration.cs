using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Serveo.Domain.Entities.Identity;

namespace Serveo.Infrastructure.Persistence.EntityFramework.Configurations.Modules.Identity
{
    internal sealed class UserTokenConfiguration : IEntityTypeConfiguration<UserToken>
    {
        public void Configure(EntityTypeBuilder<UserToken> builder)
        {
            builder.ToTable("UserTokens");
            builder.Property(u => u.Value).HasMaxLength(512);
            builder.Property(u => u.Name).HasMaxLength(512);
            builder.Property(u => u.LoginProvider).HasMaxLength(512);
        }
    }
}
