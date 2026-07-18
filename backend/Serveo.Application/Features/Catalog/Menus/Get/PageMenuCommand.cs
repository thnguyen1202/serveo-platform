using Serveo.Application.Abstractions.Mediator;
using Serveo.Application.Dtos.Catalog;
using Serveo.Application.Services;

namespace Serveo.Application.Features.Catalog.Menus.Get
{
    public sealed record PageMenuCommand(
        PageQuery Query
    ) : ICommand<PagedResult<MenuDto>>
    {
    }
}
