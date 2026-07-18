namespace Serveo.Application.Features.Tenanting.RegisterTenant
{
    public sealed record RegisterTenantResult(
        Guid TenantId,
        Guid BranchId,
        Guid UserId,
        bool RequiresEmailVerification
    );
}