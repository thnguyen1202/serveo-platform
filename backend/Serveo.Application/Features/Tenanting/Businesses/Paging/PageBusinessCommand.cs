using Serveo.Application.Abstractions.Mediator;
using Serveo.Application.Dtos.Tenanting;
using Serveo.Application.Services;

namespace Serveo.Application.Features.Tenanting.Businesses.Paging
{
    public sealed record PageBusinessCommand(
        PageQuery Query
    ) : ICommand<PagedResult<BusinessDto>>
    {
    }
}
