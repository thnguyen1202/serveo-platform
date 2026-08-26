using Serveo.Application.Abstractions.Mediator;

namespace Serveo.Application.Features.Catalog.Menus.Create
{
    public sealed record CreateMenuCommand(
        Guid BusinessId,
        string Name,
        string? Description
    ) : ICommand<ICommandResult<CreateMenuResult>>;
}
