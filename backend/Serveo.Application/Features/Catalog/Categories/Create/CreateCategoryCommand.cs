using Serveo.Application.Abstractions.Mediator;

namespace Serveo.Application.Features.Catalog.Categories.Create
{
    public sealed record CreateCategoryCommand(
        Guid BusinessId,
        string Name,
        Guid? MenuId = null // used when assigning a menu
    ) : ICommand<ICommandResult<CreateCategoryResult>>;
}
