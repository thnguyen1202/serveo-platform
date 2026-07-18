using Serveo.Application.Abstractions.Mediator;
using Serveo.Application.Dtos.Catalog;

namespace Serveo.Application.Features.Catalog.Menus.Get
{
    public sealed record PublicMenuCommand(
        Guid BusinessId
    ) : ICommand<IEnumerable<MenuDto>>
    {
    }
}
