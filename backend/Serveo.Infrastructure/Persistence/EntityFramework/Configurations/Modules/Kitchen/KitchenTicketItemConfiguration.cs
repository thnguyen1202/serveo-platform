using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Serveo.Domain.Entities.SaaS.Kitchen;

namespace Serveo.Infrastructure.Persistence.EntityFramework.Configurations.Modules.Tenant
{
    internal sealed class KitchenTicketItemConfiguration : IEntityTypeConfiguration<KitchenTicketItem>
    {
        public void Configure(EntityTypeBuilder<KitchenTicketItem> builder)
        {

        }
    }
}
