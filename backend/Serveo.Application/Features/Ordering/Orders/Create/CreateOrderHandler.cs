using AutoMapper;
using Serveo.Application.Abstractions;
using Serveo.Application.Abstractions.Mediator;
using Serveo.Application.Services;
using Serveo.Domain.Entities.Ordering;

namespace Serveo.Application.Features.Ordering.Orders.Create
{
    public sealed class CreateOrderHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper
    ) : ICommandHandler<CreateOrderCommand, ICommandResult<CreateOrderResult>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        public async Task<ICommandResult<CreateOrderResult>> HandleAsync(CreateOrderCommand request, CancellationToken ct)
        {
            var entity = new Order()
            {
            };
            _unitOfWork.SetValues(entity, request);
            await _unitOfWork.SaveChangesAsync(ct);

            return CommandResult<CreateOrderResult>.Success(_mapper.Map<CreateOrderResult>(entity));
        }
    }
}
