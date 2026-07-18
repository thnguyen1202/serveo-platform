using AutoMapper;
using Serveo.Application.Abstractions;
using Serveo.Application.Abstractions.Mediator;
using Serveo.Application.Services;
using Serveo.Domain.Entities.Catalog;

namespace Serveo.Application.Features.Catalog.Menus.Items.Create
{
    public sealed class CreateMenuItemHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper
    ) : ICommandHandler<CreateMenuItemCommand, ICommandResult<CreateMenuItemResult>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        public async Task<ICommandResult<CreateMenuItemResult>> HandleAsync(CreateMenuItemCommand request, CancellationToken ct)
        {
            var menuItems = request.ProductIds.Select(s => new MenuItem { MenuId = request.MenuId, ProductId = s });
            //_unitOfWork.Set<MenuItem>().AddRange(menuItems);
            await _unitOfWork.SaveChangesAsync(ct);

            return CommandResult<CreateMenuItemResult>.Success(new CreateMenuItemResult { Id = request.MenuId });
        }
    }
}
