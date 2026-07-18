using Serveo.Application.Abstractions.Mediator;

namespace Serveo.Application.Features.Ordering.Orders.Create
{
    public sealed record CreateOrderCommand(
        Guid TableId
    ) : ICommand<ICommandResult<CreateOrderResult>>;
}