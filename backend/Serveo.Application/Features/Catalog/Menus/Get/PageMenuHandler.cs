using Serveo.Application.Abstractions;
using Serveo.Application.Abstractions.Mediator;
using Serveo.Application.Dtos.Catalog;
using Serveo.Application.Services;

namespace Serveo.Application.Features.Catalog.Menus.Get
{
    public sealed class PageMenuHandler(
        IUnitOfWork unitOfWork,
        CommandMapper mapper
    ) : ICommandHandler<PageMenuCommand, PagedResult<MenuDto>>
    {
        public async Task<PagedResult<MenuDto>> HandleAsync(PageMenuCommand request, CancellationToken ct)
        {
            var businessId = unitOfWork.Session.BusinessId;
            var query = unitOfWork.Menus.Query();

            // filter business
            if (businessId.HasValue)
            {
                query = query.Where(x => x.BusinessId == businessId.Value);
            }

            #region Filter
            var filter = request.Query.Filter?.Trim();
            if (!string.IsNullOrWhiteSpace(filter))
            {
                query = query.Where(x => x.Name.Contains(filter));
            }
            #endregion

            #region Sorting
            query = request.Query.Sorting switch
            {
                _ => query.OrderByDescending(o => o.CreatedAt),
            };
            #endregion

            return await mapper.ProjectToDto(query)
                .ToPagedResultAsync(request.Query.PageIndex, request.Query.PageSize, ct);
        }
    }
}
