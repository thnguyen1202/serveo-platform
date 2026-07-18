using AutoMapper;
using Serveo.Application.Abstractions;
using Serveo.Application.Abstractions.Mediator;
using Serveo.Domain.Entities.Catalog;

namespace Serveo.Application.Features.Catalog.Products.Get
{
    public sealed class PublicProductHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper
    ) : ICommandHandler<PublicProductCommand, PublicProductResutl>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        public async Task<PublicProductResutl> HandleAsync(PublicProductCommand request, CancellationToken ct)
        {
            var keys = new object[] { request.Id };
            var product = await _unitOfWork.Products.FindAsync(keys, ct);

            return _mapper.Map<PublicProductResutl>(product);
        }
    }
}
