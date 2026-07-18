using Serveo.Application.Services.Administration.Accounts.Dto;
using Serveo.Domain.Entities.Identity;

namespace Serveo.Application.Services.Administration.Accounts
{
    public interface IJwtService
    {
        Task<AuthTokenResult> AuthTokenAsync(User user, string? ip);
        Task CreateRefreshTokenAsync(Guid userId, string refreshTokenHash, string? createdByIp);
        Task<string> GenerateAccessTokenAsync(User user);
        (string token, string hash) GenerateRefreshToken(int size = 64);
    }
}