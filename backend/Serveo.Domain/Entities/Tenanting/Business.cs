using Serveo.Domain.Entities.Base;

namespace Serveo.Domain.Entities.Tenanting
{
    public sealed class Business : FullAuditableEntity, IMustHaveTenant
    {
        public Guid TenantId { get; set; }


        public string Name { get; set; } = default!;
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public BusinessType? Type { get; set; }
        public byte Status { get; set; }


        public Tenant Tenant { get; set; } = default!;
    }

    public enum BusinessType
    {
        Restaurant,
        Cafe,
        Spa,
        Retail,
        Salon,
        Other
    }
}
