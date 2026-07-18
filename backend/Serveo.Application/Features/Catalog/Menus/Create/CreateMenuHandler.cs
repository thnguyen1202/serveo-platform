using AutoMapper;
using Serveo.Application.Abstractions;
using Serveo.Application.Abstractions.Mediator;
using Serveo.Application.Services;

namespace Serveo.Application.Features.Catalog.Menus.Create
{
    public sealed class CreateMenuHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper
    ) : ICommandHandler<CreateMenuCommand, ICommandResult<CreateMenuResult>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        public async Task<ICommandResult<CreateMenuResult>> HandleAsync(CreateMenuCommand request, CancellationToken ct)
        {
            var entity = new Serveo.Domain.Entities.Catalog.Menu();
            _unitOfWork.SetValues(entity, request);
            await _unitOfWork.SaveChangesAsync(ct);

            return CommandResult<CreateMenuResult>.Success(_mapper.Map<CreateMenuResult>(entity));
        }
    }
}
