using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Serveo.Domain.Entities.Tenanting;

namespace Serveo.Infrastructure.Persistence.EntityFramework.Configurations.Modules.Tenanting
{
    internal sealed class TenantMemberBranchConfiguration : IEntityTypeConfiguration<TenantMemberBranch>
    {
        public void Configure(EntityTypeBuilder<TenantMemberBranch> builder)
        {
            builder.HasKey(x => new
            {
                x.TenantMemberId,
                x.BranchId
            });

            // Maintain Cascade on the primary parent relationship
            builder.HasOne(x => x.TenantMember)
                .WithMany(x => x.TenantMemberBranches)
                .HasForeignKey(x => x.TenantMemberId)
                .OnDelete(DeleteBehavior.Cascade);

            // Change secondary relationship to Restrict or NoAction
            builder.HasOne(x => x.Branch)
                .WithMany(x => x.TenantMemberBranches)
                .HasForeignKey(x => x.BranchId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
