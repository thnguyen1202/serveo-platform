using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Serveo.Domain.Entities.Authorization;

namespace Serveo.Infrastructure.Persistence.EntityFramework.Configurations.Modules.Authorization
{
    internal sealed class UserNotificationConfiguration : IEntityTypeConfiguration<UserNotification>
    {
        public void Configure(EntityTypeBuilder<UserNotification> builder)
        {
            builder.Property(u => u.TargetNotifiers).HasMaxLength(1024);
        }
    }
}
