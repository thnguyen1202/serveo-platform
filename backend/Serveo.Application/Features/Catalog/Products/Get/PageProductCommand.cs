using Serveo.Application.Abstractions.Mediator;
using Serveo.Application.Dtos.Catalog;
using Serveo.Application.Services;

namespace Serveo.Application.Features.Catalog.Products.Get
{
    public sealed record PageProductCommand(
        PageQuery Query
    ) : ICommand<PagedResult<ProductDto>>
    {
    }
}
