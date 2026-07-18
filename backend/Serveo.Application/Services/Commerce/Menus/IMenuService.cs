using Serveo.Application.Dtos;
using Serveo.Application.Services.Commerce.Menus.Dto;

namespace Serveo.Application.Services.Commerce.Menus
{
    public interface IMenuService : IService
    {
        Task<PagedResult<MenuDto>> PageAsync(PageQueryDto input);
        //Task<CommandResult> CreateAsync(CreateMenuDto input, CancellationToken ct = default);
        //Task<CommandResult> UpdateAsync(UpdateMenuDto input, CancellationToken ct = default);
        //Task<CommandResult> UpdateStatusAsync(UpdateStatusMenuDto input, CancellationToken ct = default);
        Task<bool> IsDuplicateNameAsync(string name, Guid? dinerId = null, Guid? id = null);
        //Task<CommandResult> DeleteAsync(object? id, CancellationToken ct = default);
        Task<MenuDto?> FindAsync(Guid? id, CancellationToken ct = default);
    }
}