using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Serveo.Application.Abstractions;
using Serveo.Application.Abstractions.Mediator;
using Serveo.Application.Dtos.Tenanting;
using Serveo.Application.Services;

namespace Serveo.Application.Features.Tenanting.Tenants.Paging
{
    public sealed class PageTenantHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper
    ) : ICommandHandler<PageTenantCommand, PagedResult<TenantDto>>
    {

        public async Task<PagedResult<TenantDto>> HandleAsync(PageTenantCommand request, CancellationToken ct)
        {
            var query = unitOfWork.Tenants.Query();

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

            return new PagedResult<TenantDto>(mapper.Map<IReadOnlyList<TenantDto>>(items), itemCount, request.Query.PageIndex, request.Query.PageSize);
        }
    }
}
