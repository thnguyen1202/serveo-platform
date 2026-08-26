using Serveo.Application.Abstractions.Mediator;
using Serveo.Domain.Entities.Identity;

namespace Serveo.Application.Features.Identity.Auth.RefreshToken
{
    public sealed record RefreshTokenCommand(
        string RefreshToken,
        ClientType ClientType,
        string DeviceId,
        string IpAddress,
        string UserAgent
    ) : ICommand<ICommandResult<RefreshTokenResult>>;
}
