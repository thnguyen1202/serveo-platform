using Serveo.Application.Abstractions.Mediator;

namespace Serveo.Application.Features.Platform.CreateApiKey
{
    public sealed record CreateApiKeyCommand(
        string ClientName,
        string KeyType = "live"
    ) : ICommand<ICommandResult<CreateApiKeyResult>>;
}
