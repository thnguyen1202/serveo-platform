using Serveo.Application.Abstractions.Mediator;

namespace Serveo.Application.Features.Ordering.OrderSessions.Create
{
    public sealed record CreateOrderSessionCommand(
        Guid TableId
    ) : ICommand<ICommandResult<CreateOrderSessionResult>>;
}