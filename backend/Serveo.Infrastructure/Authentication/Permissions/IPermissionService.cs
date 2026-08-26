namespace Serveo.Infrastructure.Authentication.Permissions
{
    public interface IPermissionService
    {
        Task<bool> HasPermissionAsync(Guid userId, Guid tenantId, string permission, CancellationToken cancellationToken = default);
    }
}
