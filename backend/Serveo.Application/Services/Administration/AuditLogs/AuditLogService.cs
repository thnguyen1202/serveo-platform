using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Serveo.Application.Dtos;
using Serveo.Application.Services.Administration.AuditLogs.Dto;
using Serveo.Domain.Entities.Authorization;
using Serveo.Domain.Interfaces;

namespace Serveo.Application.Services.Administration.AuditLogs
{
    using CrudEntity = AuditLog;
    using EntityDto = AuditLogDto;

    public class AuditLogService : IAuditLogService, IService
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        private const string _entityName = nameof(AuditLog);
        private readonly DbSet<CrudEntity> _entities;

        public AuditLogService(ILogger<AuditLogService> logger, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _entities = _unitOfWork.Set<CrudEntity>();
        }

        public async Task<PagedResult<AuditLogDto>> PageAsync(PageQueryDto input)
        {
            var query = _entities.AsQueryable();

            #region Sorting
            query = input.Sorting switch
            {
                _ => query.OrderByDescending(o => o.Id),
            };
            #endregion

            #region Filter
            if (!string.IsNullOrWhiteSpace(input.Filter))
            {
                query = query.Where(x => x.Action.Contains(input.Filter) || x.TableName.Contains(input.Filter));
            }
            #endregion

            #region Paged
            var itemCount = await query.CountAsync();
            var items = query
                    .Skip((input.PageIndex - 1) * input.PageSize)
                    .Take(input.PageSize)
                    .AsEnumerable();
            #endregion

            return new PagedResult<EntityDto>(_mapper.Map<IReadOnlyList<EntityDto>>(items), itemCount);
        }


    }
}
