using Serveo.Application.Abstractions.Mediator;

namespace Serveo.Application.Features.Ordering.Tables.Create
{
    public sealed record CreateTableCommand(
        Guid BranchId,
        string Name,
        int Capacity
    ) : ICommand<ICommandResult<CreateTableResult>>;
}
