using Serveo.Domain.Entities.Tenanting;

namespace Serveo.Application.Dtos.Tenanting
{
    public sealed class TenantDto : EntityDto
    {
        public string Name { get; set; } = default!;
        public TenantBillingType BillingType { get; set; }
        public DateTimeOffset ExpiredTime { get; set; }
        public byte Status { get; set; }
    }
}
