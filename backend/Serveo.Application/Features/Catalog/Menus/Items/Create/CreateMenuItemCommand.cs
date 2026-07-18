using Serveo.Application.Abstractions.Mediator;

namespace Serveo.Application.Features.Catalog.Menus.Items.Create
{
    public sealed record CreateMenuItemCommand(
        Guid MenuId,
        List<Guid> ProductIds
    ) : ICommand<ICommandResult<CreateMenuItemResult>>;
}
