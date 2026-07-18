using Serveo.Application.Dtos;
using Serveo.Application.Services.Commerce.Products.Dto;

namespace Serveo.Application.Services.Commerce.Products
{
    public interface IProductService
    {
        Task<PagedResult<ProductDto>> PageAsync(PageQueryDto input);
        //Task<CommandResult> CreateAsync(CreateProductDto input, CancellationToken ct = default);
        //Task<CommandResult> UpdateAsync(UpdateProductDto input, CancellationToken ct = default);
        Task<ProductDto?> FindAsync(object? id, CancellationToken ct = default);
        //Task<CommandResult> DeleteAsync(object? id, CancellationToken ct = default);
        //Task<CommandResult> UpdateStatusAsync(UpdateStatusProductDto input, CancellationToken ct = default);
        Task<bool> IsDuplicateNameAsync(string name, Guid? categoryId = null, Guid? id = null);
    }
}