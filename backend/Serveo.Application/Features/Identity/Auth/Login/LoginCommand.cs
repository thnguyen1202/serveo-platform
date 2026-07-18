using Serveo.Application.Abstractions.Mediator;

namespace Serveo.Application.Features.Identity.Auth.Login
{
    public sealed record LoginCommand(
        string Email,
        string Password,
        string? Ip
    ) : ICommand<ICommandResult<LoginResult>>;
}
