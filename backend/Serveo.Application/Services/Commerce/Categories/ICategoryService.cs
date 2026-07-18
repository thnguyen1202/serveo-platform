using Serveo.Application.Dtos;
using Serveo.Application.Services.Commerce.Categories.Dto;

namespace Serveo.Application.Services.Commerce.Categories
{
    public interface ICategoryService
    {
        //Task<CommandResult> CreateAsync(CreateCategoryDto input, CancellationToken ct = default);
        //Task<CommandResult> DeleteAsync(object? id, CancellationToken ct = default);
        Task<CategoryDto?> FindAsync(Guid id, CancellationToken ct = default);
        Task<bool> IsDuplicateNameAsync(string name, Guid? dinerId = null, Guid? id = null);
        Task<PagedResult<CategoryDto>> PageAsync(PageQueryDto input);
        //Task<CommandResult> UpdateAsync(UpdateCategoryDto input, CancellationToken ct = default);
        //Task<CommandResult> UpdateStatusAsync(UpdateStatusCategoryDto input, CancellationToken ct = default);
    }
}