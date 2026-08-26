using Serveo.Application.Abstractions.Mediator;
using Serveo.Application.Dtos.Tenanting;
using Serveo.Application.Services;

namespace Serveo.Application.Features.Tenanting.Tenants.Paging
{
    public sealed record PageTenantCommand(
        PageQuery Query
    ) : ICommand<PagedResult<TenantDto>>
    {
    }
}
