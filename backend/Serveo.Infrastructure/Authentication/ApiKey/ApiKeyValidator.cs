using Microsoft.EntityFrameworkCore;
using Serveo.Application.Abstractions;
using Serveo.Domain.Entities.Authorization;
using System.Security.Cryptography;
using System.Text;

namespace Serveo.Infrastructure.Authentication.ApiKey
{
    public class ApiKeyValidator(IUnitOfWork unitOfWork) : IApiKeyValidator
    {
        public async Task<ApiKeyValidationResult> ValidateAsync(string apiKey, CancellationToken ct)
        {
            var keyHash = Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(apiKey)));
            var result = await unitOfWork.Set<ApiClientKey>().Where(x => x.KeyHash == keyHash).Select(x => new ApiKeyValidationResult
            (
                true,
                x.ClientId,
                x.Client.TenantId,
                null
            )).FirstOrDefaultAsync(ct);

            if (result == null)
            {
                return new ApiKeyValidationResult
                (
                    false,
                    null,
                    null,
                    "Invalid API key."
                );
            }

            return result;
        }
    }
}
