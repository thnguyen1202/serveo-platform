using Serveo.Application.Abstractions.Mediator;
using Serveo.Application.Dtos.Tenanting;

namespace Serveo.Application.Features.Tenanting.Tenant.Profile
{
    public sealed record GetTenantProfileCommand(
        Guid Id
    ) : ICommand<ICommandResult<TenantDto>>;
}
