using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Serveo.Domain.Entities.SaaS.Kitchen;

namespace Serveo.Infrastructure.Persistence.EntityFramework.Configurations.Modules.Tenant
{
    internal sealed class KitchenTicketConfiguration : IEntityTypeConfiguration<KitchenTicket>
    {
        public void Configure(EntityTypeBuilder<KitchenTicket> builder)
        {
            
        }
    }
}
