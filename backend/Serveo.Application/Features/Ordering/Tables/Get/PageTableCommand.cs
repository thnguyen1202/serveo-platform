using Serveo.Application.Abstractions.Mediator;
using Serveo.Application.Dtos.Ordering;
using Serveo.Application.Services;

namespace Serveo.Application.Features.Ordering.Tables.Get
{
    public sealed record PageTableCommand(
        PageQuery Query
    ) : ICommand<PagedResult<TableDto>>
    {
    }
}
