using Serveo.Application.Abstractions.Mediator;
using Serveo.Application.Dtos.Catalog;

namespace Serveo.Application.Features.Catalog.Menus.Get
{
    public sealed record MenuDetailsCommand(
        Guid MenuId,
        bool IsFull
    ) : ICommand<ICommandResult<MenuDto>>;
}
