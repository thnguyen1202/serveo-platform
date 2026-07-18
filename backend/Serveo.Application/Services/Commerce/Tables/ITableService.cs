using Serveo.Application.Dtos;
using Serveo.Application.Services.Commerce.Tables.Dto;

namespace Serveo.Application.Services.Commerce.Tables
{
    public interface ITableService
    {
        //Task<CommandResult> CreateAsync(CreateTableDto input, CancellationToken ct = default);
        //Task<CommandResult> DeleteAsync(object? id, CancellationToken ct = default);
        Task<TableDto?> FindAsync(Guid? id, CancellationToken ct = default);
        Task<bool> IsDuplicateNameAsync(string name, Guid? dinerId = null, Guid? id = null);
        Task<PagedResult<TableDto>> PageAsync(PageQueryDto input);
        //Task<CommandResult> UpdateAsync(UpdateTableDto input, CancellationToken ct = default);
        //Task<CommandResult> UpdateStatusAsync(UpdateStatusTableDto input, CancellationToken ct = default);
    }
}