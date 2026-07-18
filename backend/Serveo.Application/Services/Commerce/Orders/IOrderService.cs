using Serveo.Application.Dtos;
using Serveo.Application.Services.Commerce.Orders.Dto;

namespace Serveo.Application.Services.Commerce.Orders
{
    public interface IOrderService : IService
    {
        Task<PagedResult<OrderDto>> PageAsync(PageQueryDto input);
        //Task<CommandResult> CreateAsync(CreateOrderDto input, CancellationToken ct = default);
        //Task<CommandResult> UpdateAsync(UpdateOrderDto input, CancellationToken ct = default);
        //Task<CommandResult> UpdateStatusAsync(UpdateStatusOrderDto input, CancellationToken ct = default);
        //Task<CommandResult> DeleteAsync(object? id, CancellationToken ct = default);
        Task<OrderDto?> FindAsync(object? id, CancellationToken ct = default);
    }
}