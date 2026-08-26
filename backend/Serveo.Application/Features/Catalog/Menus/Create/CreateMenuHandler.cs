using Serveo.Application.Abstractions;
using Serveo.Application.Abstractions.Mediator;
using Serveo.Application.Services;
using Serveo.Domain.Entities.Catalog;

namespace Serveo.Application.Features.Catalog.Menus.Create
{
    public sealed class CreateMenuHandler(
        IUnitOfWork unitOfWork,
        CommandMapper mapper
    ) : ICommandHandler<CreateMenuCommand, ICommandResult<CreateMenuResult>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

        public async Task<ICommandResult<CreateMenuResult>> HandleAsync(CreateMenuCommand request, CancellationToken ct)
        {
            var entity = new Menu();

            _unitOfWork.SetValues(entity, request);
            _unitOfWork.Add(entity);
            await _unitOfWork.SaveChangesAsync(ct);

            return CommandResult<CreateMenuResult>.Success(mapper.ToResult(entity));
        }
    }
}
