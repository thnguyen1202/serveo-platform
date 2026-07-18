using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Serveo.Application.Abstractions;
using Serveo.Application.Abstractions.Mediator;
using Serveo.Application.Dtos.Catalog;
using Serveo.Application.Services;
using Serveo.Domain.Entities.Catalog;

namespace Serveo.Application.Features.Catalog.Menus.Get
{
    public sealed class PageMenuHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper
    ) : ICommandHandler<PageMenuCommand, PagedResult<MenuDto>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        public async Task<PagedResult<MenuDto>> HandleAsync(PageMenuCommand request, CancellationToken ct)
        {
            var query = _unitOfWork.Menus.Query();

            #region Sorting
            query = request.Query.Sorting switch
            {
                _ => query.OrderByDescending(o => o.CreatedAt),
            };
            #endregion

            #region Filter
            if (!string.IsNullOrWhiteSpace(request.Query.Filter))
            {
                query = query.Where(x => !string.IsNullOrWhiteSpace(x.Name) && x.Name.Contains(request.Query.Filter));
            }
            #endregion

            #region Paged
            var itemCount = await query.CountAsync(ct);
            var items = query
                    .Skip((request.Query.PageIndex - 1) * request.Query.PageSize)
                    .Take(request.Query.PageSize)
                    .AsEnumerable();
            #endregion

            return new PagedResult<MenuDto>(_mapper.Map<IReadOnlyList<MenuDto>>(items), itemCount);
        }
    }
}
