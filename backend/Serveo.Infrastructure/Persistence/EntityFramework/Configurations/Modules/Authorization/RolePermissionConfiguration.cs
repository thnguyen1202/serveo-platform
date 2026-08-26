using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Serveo.Domain.Entities.Authorization;

namespace Serveo.Infrastructure.Persistence.EntityFramework.Configurations.Modules.Authorization
{
    internal sealed class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
    {
        public void Configure(EntityTypeBuilder<RolePermission> builder)
        {
            builder.HasKey(x => new
            {
                x.RoleId,
                x.PermissionId
            });
        }
    }
}
