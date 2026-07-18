using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Serveo.Domain.Entities.Identity;

namespace Serveo.Infrastructure.Persistence.EntityFramework.Configurations.Modules.Identity
{
    internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.Property(u => u.SecurityStamp).HasMaxLength(128);
            builder.Property(u => u.ConcurrencyStamp).HasMaxLength(128);
            builder.Property(u => u.PhoneNumber).HasMaxLength(32);

            builder.Property(u => u.DisplayName).HasMaxLength(256);
            builder.Property(u => u.Avatar).HasMaxLength(512);
            builder.Property(u => u.TimeZoneId).HasMaxLength(128);
        }
    }
}
