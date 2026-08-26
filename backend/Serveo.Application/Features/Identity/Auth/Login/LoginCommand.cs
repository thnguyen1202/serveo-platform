using Serveo.Application.Abstractions.Mediator;
using Serveo.Domain.Entities.Identity;

namespace Serveo.Application.Features.Identity.Auth.Login
{
    public sealed record LoginCommand(
        string Email,
        string Password,
        bool IsRemember,
        ClientType ClientType,
        string DeviceId,
        string IpAddress,
        string UserAgent
    ) : ICommand<ICommandResult<LoginResult>>
    {
    }
}
