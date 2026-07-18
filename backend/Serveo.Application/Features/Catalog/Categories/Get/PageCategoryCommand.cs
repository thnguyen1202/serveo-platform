using Serveo.Application.Abstractions.Mediator;
using Serveo.Application.Dtos.Catalog;
using Serveo.Application.Services;

namespace Serveo.Application.Features.Catalog.Categories.Get
{
    public sealed record PageCategoryCommand(
        PageQuery Query
    ) : ICommand<PagedResult<CategoryDto>>
    {
    }
}
