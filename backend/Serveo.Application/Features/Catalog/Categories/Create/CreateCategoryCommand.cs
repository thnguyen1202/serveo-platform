using Serveo.Application.Abstractions.Mediator;

namespace Serveo.Application.Features.Catalog.Categories.Create
{
    public sealed record CreateCategoryCommand(
        Guid BusinessId,
        string Name
    ) : ICommand<ICommandResult<CreateCategoryResult>>;
}
