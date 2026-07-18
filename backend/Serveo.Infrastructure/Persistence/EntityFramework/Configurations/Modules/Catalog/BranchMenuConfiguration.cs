using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Serveo.Domain.Entities.Catalog;

namespace Serveo.Infrastructure.Persistence.EntityFramework.Configurations.Modules.Catalog
{
    internal sealed class BranchMenuConfiguration : IEntityTypeConfiguration<BranchMenu>
    {
        public void Configure(EntityTypeBuilder<BranchMenu> builder)
        {
            builder.HasKey(x => new
            {
                x.BranchId,
                x.MenuId
            });
        }
    }
}
