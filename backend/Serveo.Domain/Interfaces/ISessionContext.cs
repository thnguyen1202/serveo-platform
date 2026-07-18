namespace Serveo.Domain.Interfaces
{
    public interface ISessionContext
    {
        Guid? TenantId { get; }
        Guid? UserId { get; }
        Guid? BusinessId { get; }
        Guid? BranchId { get; }
        bool IsAuthenticated { get; }
        bool IsSuperAdmin { get; }
    }
}
