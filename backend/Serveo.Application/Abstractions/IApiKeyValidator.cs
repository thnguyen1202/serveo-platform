namespace Serveo.Application.Abstractions
{
    public interface IApiKeyValidator
    {
        Task<ApiKeyValidationResult> ValidateAsync(string apiKey, CancellationToken ct);
    }

    public sealed record ApiKeyValidationResult(
        bool Succeeded,
        Guid? ClientId = null,
        Guid? TenantId = null,
        string? FailureReason = null
    );
}
