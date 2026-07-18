using Serveo.Application.Dtos;
using Serveo.Application.Services.Commerce.Staffs.Dto;

namespace Serveo.Application.Services.Commerce.Staffs
{
    public interface IStaffService
    {
        //Task<CommandResult> CreateAsync(CreateStaffDto input, CancellationToken ct = default);
        //Task<CommandResult> DeleteAsync(object? id, CancellationToken ct = default);
        Task<StaffDto?> FindAsync(object? id, CancellationToken ct = default);
        Task<bool> IsDuplicateEmailAsync(string email, Guid? id = null);
        Task<PagedResult<StaffDto>> PageAsync(PageQueryDto input);
        //Task<CommandResult> UpdateAsync(UpdateStaffDto input, CancellationToken ct = default);
        //Task<CommandResult> UpdateStatusAsync(UpdateStatusStaffDto input, CancellationToken ct = default);
    }
}