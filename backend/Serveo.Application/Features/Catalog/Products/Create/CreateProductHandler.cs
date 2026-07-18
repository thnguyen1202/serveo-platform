using AutoMapper;
using Serveo.Application.Abstractions;
using Serveo.Application.Abstractions.Mediator;
using Serveo.Application.Services;
using Serveo.Domain.Entities.Catalog;

namespace Serveo.Application.Features.Catalog.Products.Create
{
    public sealed class CreateProductHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper
    ) : ICommandHandler<CreateProductCommand, ICommandResult<CreateProductResult>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        public async Task<ICommandResult<CreateProductResult>> HandleAsync(CreateProductCommand request, CancellationToken ct)
        {
            var entity = new Product();
            _unitOfWork.SetValues(entity, request);
            await _unitOfWork.SaveChangesAsync(ct);

            return CommandResult<CreateProductResult>.Success(_mapper.Map<CreateProductResult>(entity));
        }
    }
}
