using Serveo.Application.Dtos;
using Serveo.Application.Services.Administration.AuditLogs.Dto;

namespace Serveo.Application.Services.Administration.AuditLogs
{
    public interface IAuditLogService
    {
        Task<PagedResult<AuditLogDto>> PageAsync(PageQueryDto input);
    }
}