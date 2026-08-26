using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Serveo.Domain.Entities.Identity;

namespace Serveo.Infrastructure.Persistence.EntityFramework.Configurations.Modules.Identity
{
    internal sealed class UserSessionConfiguration : IEntityTypeConfiguration<UserSession>
    {
        public void Configure(EntityTypeBuilder<UserSession> builder)
        {
            builder.ToTable("UserSessions");
            builder.Property(u => u.DeviceId).HasMaxLength(64);
            //builder.Property(u => u.DeviceName).HasMaxLength(64);
            //builder.Property(u => u.Browser).HasMaxLength(64);
            //builder.Property(u => u.OperatingSystem).HasMaxLength(64);
            builder.Property(u => u.IpAddress).HasMaxLength(64);
            builder.Property(u => u.UserAgent).HasMaxLength(256);
            builder.Property(u => u.RefreshTokenHash).HasMaxLength(512);
            builder.Property(u => u.PreviousTokenHash).HasMaxLength(512);

            builder.HasIndex(x => new { x.UserId, x.DeviceId, x.ClientType }).IsUnique();
        }
    }
}
