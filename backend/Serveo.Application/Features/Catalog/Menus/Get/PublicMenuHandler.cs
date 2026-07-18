using AutoMapper;
using Serveo.Application.Abstractions;
using Serveo.Application.Abstractions.Mediator;
using Serveo.Application.Dtos.Catalog;
using Serveo.Domain.Entities.Catalog;

namespace Serveo.Application.Features.Catalog.Menus.Get
{
    public sealed class PublicMenuHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper
    ) : ICommandHandler<PublicMenuCommand, IEnumerable<MenuDto>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        public async Task<IEnumerable<MenuDto>> HandleAsync(PublicMenuCommand request, CancellationToken ct)
        {
            var query = _unitOfWork.Menus.Query()
                //.Include(x => x.Items).ThenInclude(x => x.Product)
                .Where(x => x.BusinessId == request.BusinessId);

            var menus = query.ToList();
            var menuDtos = _mapper.Map<IEnumerable<MenuDto>>(menus);
            return _mapper.Map<IEnumerable<MenuDto>>(query);
        }
    }
}
