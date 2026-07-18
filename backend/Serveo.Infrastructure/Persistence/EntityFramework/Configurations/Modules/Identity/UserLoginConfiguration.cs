using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Serveo.Domain.Entities.Identity;

namespace Serveo.Infrastructure.Persistence.EntityFramework.Configurations.Modules.Identity
{
    internal sealed class UserLoginConfiguration : IEntityTypeConfiguration<UserLogin>
    {
        public void Configure(EntityTypeBuilder<UserLogin> builder)
        {
            builder.ToTable("UserLogins");
            builder.Property(u => u.ProviderDisplayName).HasMaxLength(256);
            builder.Property(u => u.LoginProvider).HasMaxLength(512);
            builder.Property(u => u.ProviderKey).HasMaxLength(512);
        }
    }
}
