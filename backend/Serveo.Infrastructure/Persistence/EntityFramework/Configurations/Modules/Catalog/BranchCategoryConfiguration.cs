using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Serveo.Domain.Entities.Catalog;

namespace Serveo.Infrastructure.Persistence.EntityFramework.Configurations.Modules.Catalog
{
    internal sealed class BranchCategoryConfiguration : IEntityTypeConfiguration<BranchCategory>
    {
        public void Configure(EntityTypeBuilder<BranchCategory> builder)
        {
            builder.HasKey(x => new
            {
                x.BranchId,
                x.CategoryId
            });
        }
    }
}
