using Serveo.Application.Abstractions;
using Serveo.Application.Abstractions.Mediator;
using Serveo.Application.Services;
using Serveo.Domain.Entities.Authorization;

namespace Serveo.Application.Features.Platform.CreateApiKey
{
    public sealed class CreateApiKeyHandler(
        IUnitOfWork unitOfWork
    ) : ICommandHandler<CreateApiKeyCommand, ICommandResult<CreateApiKeyResult>>
    {

        public async Task<ICommandResult<CreateApiKeyResult>> HandleAsync(CreateApiKeyCommand request, CancellationToken ct)
        {
            var value = await unitOfWork.ExecuteAsync(async () =>
            {
                // create ApiClient
                var client = new ApiClient
                {
                    Name = request.ClientName,
                    Type = ApiClientType.External
                };
                unitOfWork.Add(client);

                // create ApiClientKey
                var (key, keyHash) = ApiClientKey.GenerateKey(request.KeyType);
                var clientKey = new ApiClientKey
                {
                    ClientId = client.Id,
                    KeyHash = keyHash,
                    ExpiresAt = DateTime.UtcNow.AddYears(8)
                };
                unitOfWork.Add(clientKey);

                return new CreateApiKeyResult
                {
                    Key = key
                };
            }, ct);

            return CommandResult<CreateApiKeyResult>.Success(value);
        }
    }
}
