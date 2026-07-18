using Serveo.Application.Abstractions.Mediator;

namespace Serveo.Application.Features.Tenanting.Branches.Create
{
    public sealed record CreateBranchCommand(
        string Name,
        string? Address,
        Guid? BusinessId = null
    ) : ICommand<ICommandResult<CreateBranchResult>>;
}
