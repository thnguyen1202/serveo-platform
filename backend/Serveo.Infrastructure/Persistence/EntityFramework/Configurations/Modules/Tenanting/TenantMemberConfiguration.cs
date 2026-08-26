using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Serveo.Domain.Entities.Tenanting;

namespace Serveo.Infrastructure.Persistence.EntityFramework.Configurations.Modules.Tenanting
{
    internal sealed class TenantMemberConfiguration : IEntityTypeConfiguration<TenantMember>
    {
        public void Configure(EntityTypeBuilder<TenantMember> builder)
        {
            builder.HasIndex(x => new
            {
                x.TenantId,
                x.UserId
            })
            .IsUnique();
        }
    }
}
