using Serveo.Domain.Entities.Base;

namespace Serveo.Domain.Entities.Authorization
{
    public sealed class ApiClient : Entity
    {
        public string Name { get; set; } = default!;
        public Guid? TenantId { get; set; }
        public Guid? BusiniessId { get; set; }
        public Guid? BranchId { get; set; }
        public ApiClientType Type { get; set; }

        public ICollection<ApiClientKey> ClientKeys { get; set; } = default!;
    }

    public enum ApiClientType
    {
        Internal,
        External,
        Partner,
        POS
    }
}
