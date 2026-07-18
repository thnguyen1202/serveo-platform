using Serveo.Application.Abstractions.Mediator;

namespace Serveo.Application.Features.Tenanting.RegisterTenant
{
    public sealed record RegisterTenantCommand(
        string TenantName,
        string OwnerName,
        string Email,
        string Password,
        string? Phone = null,
        string? BranchName = null,
        string? Address = null,
        string? TimeZone = null
    ) : ICommand<RegisterTenantResult>
    {
        public RegisterTenantCommand() : this("", "", "", "") { }
    }
}
