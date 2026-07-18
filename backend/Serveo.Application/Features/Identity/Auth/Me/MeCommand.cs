using Serveo.Application.Abstractions.Mediator;
using Serveo.Application.Dtos.Identity;

namespace Serveo.Application.Features.Identity.Auth.Me
{
    public sealed record MeCommand(
    ) : ICommand<ICommandResult<UserDto>>;
}
