using Serveo.Application.Abstractions.Mediator;

namespace Serveo.Application.Features.Catalog.Products.Get
{
    public sealed record PublicProductCommand(
        Guid Id
    ) : ICommand<PublicProductResutl>
    {
    }
}
