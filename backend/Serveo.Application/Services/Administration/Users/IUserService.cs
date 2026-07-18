using Serveo.Application.Dtos;
using Serveo.Application.Services.Administration.Users.Dto;

namespace Serveo.Application.Services.Administration.Users
{
    public interface IUserService
    {
        //Task<CommandResult> CreateAsync(CreateUserDto input, CancellationToken ct = default);
        //Task<CommandResult> DeleteAsync(object? id, CancellationToken ct = default);
        Task<UserDto?> FindAsync(object? id, CancellationToken ct = default);
        Task<bool> IsDuplicateEmailAsync(string email, Guid? id = null);
        Task<bool> IsDuplicateUserNameAsync(string name, Guid? id = null);
        Task<PagedResult<UserDto>> PageAsync(PageQueryDto input);
        //Task<CommandResult> UpdateAsync(UpdateUserDto input, CancellationToken ct = default);
        //Task<CommandResult> UpdateStatusAsync(UpdateStatusUserDto input, CancellationToken ct = default);
    }
}