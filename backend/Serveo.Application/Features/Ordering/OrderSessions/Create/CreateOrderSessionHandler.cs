using AutoMapper;
using Serveo.Application.Abstractions;
using Serveo.Application.Abstractions.Mediator;
using Serveo.Application.Services;
using Serveo.Domain.Entities.Ordering;

namespace Serveo.Application.Features.Ordering.OrderSessions.Create
{
    public sealed class CreateOrderSessionHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper
    ) : ICommandHandler<CreateOrderSessionCommand, ICommandResult<CreateOrderSessionResult>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        public async Task<ICommandResult<CreateOrderSessionResult>> HandleAsync(CreateOrderSessionCommand request, CancellationToken ct)
        {
            var entity = new OrderSession()
            {
            };
            _unitOfWork.SetValues(entity, request);
            await _unitOfWork.SaveChangesAsync(ct);

            return CommandResult<CreateOrderSessionResult>.Success(_mapper.Map<CreateOrderSessionResult>(entity));
        }
    }
}
