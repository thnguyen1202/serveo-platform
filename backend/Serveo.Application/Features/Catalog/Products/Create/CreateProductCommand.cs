using Serveo.Application.Abstractions.Mediator;

namespace Serveo.Application.Features.Catalog.Products.Create
{
    public sealed record CreateProductCommand(
        Guid BusinessId,
        Guid CategoryId,
        string Name,
        decimal Price,
        string? Description = null,
        string? ImageUrl = null
    ) : ICommand<ICommandResult<CreateProductResult>>;
}
