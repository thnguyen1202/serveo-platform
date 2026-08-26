using Serveo.Domain.Entities.Identity;

namespace Serveo.Application.Abstractions
{
    public interface ITokenService
    {
        Task<TokenPair> CreateTokenPairAsync(User user, ClientType clientType, string? deviceId, string? ipAddress, string? userAgent, bool isRemember, CancellationToken ct);
        string HashToken(string token);
        Task<TokenPair> RefreshTokenPairAsync(User user, UserSession userSession, string? ipAddress, string? userAgent, CancellationToken ct);
    }

    public sealed record TokenPair(
        string AccessToken,
        string RefreshToken,
        int ExpiresInSeconds
    );
}
