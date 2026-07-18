using Serveo.Domain.Entities.Tenanting;

namespace Serveo.Application.Dtos.Tenanting
{
    public sealed class BusinessDto : EntityDto
    {
        public string Name { get; set; } = default!;
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public BusinessType? Type { get; set; }
        public byte Status { get; set; }

        public string Currency { get; init; } = "VND";
        public string Language { get; init; } = "vi";

        public TenantDto? Tenant { get; set; } = null;
    }
}
