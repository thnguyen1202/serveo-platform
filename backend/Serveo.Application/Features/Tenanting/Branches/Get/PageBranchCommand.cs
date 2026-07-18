using Serveo.Application.Abstractions.Mediator;
using Serveo.Application.Dtos.Tenanting;
using Serveo.Application.Services;

namespace Serveo.Application.Features.Tenanting.Branches.Get
{
    public sealed record PageBranchCommand(
        PageQuery Query
    ) : ICommand<PagedResult<BranchDto>>
    {
    }
}
