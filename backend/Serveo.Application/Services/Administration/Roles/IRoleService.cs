using Serveo.Application.Dtos;
using Serveo.Application.Services.Administration.Roles.Dto;

namespace Serveo.Application.Services.Administration.Roles
{
    public interface IRoleService
    {
        //Task<CommandResult> CreateAsync(CreateRoleDto input, CancellationToken ct = default);
        //Task<CommandResult> DeleteAsync(object? id, CancellationToken ct = default);
        Task<RoleDto?> FindAsync(object? id, CancellationToken ct = default);
        Task<List<string?>> GetPermissionsAsync(Guid roleId);
        Task<bool> IsDuplicateNameAsync(string name, Guid? tenantId = null, Guid? id = null);
        Task<PagedResult<RoleDto>> PageAsync(PageQueryDto input);
        //Task<CommandResult> UpdateAsync(UpdateRoleDto input, CancellationToken ct = default);
        //Task<CommandResult> UpdateClaimsAsync(UpdateClaimsDto input, CancellationToken ct);
        //Task<CommandResult> UpdateStatusAsync(UpdateStatusRoleDto input, CancellationToken ct = default);
    }
}