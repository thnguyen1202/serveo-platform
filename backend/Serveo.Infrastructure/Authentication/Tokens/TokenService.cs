using Microsoft.Extensions.Options;
using Serveo.Application.Abstractions;
using Serveo.Domain.Entities.Identity;
using Serveo.Infrastructure.Authentication.Jwt;

namespace Serveo.Infrastructure.Authentication.Tokens
{
    public sealed class TokenService(
        IUnitOfWork unitOfWork,
        IJwtTokenGenerator jwtTokenGenerator,
        IRefreshTokenFactory refreshTokenFactory,
        IOptions<JwtOptions> jwtOptions) : ITokenService
    {
        private readonly JwtOptions _jwtOptions = jwtOptions.Value;

        public async Task<TokenPair> CreateTokenPairAsync(
            User user,
            string clientId,
            string? deviceId,
            string? deviceName,
            string? ipAddress,
            string? userAgent,
            CancellationToken ct)
        {
            //var roles = user.Roles.Select(x => x.Role.Name).ToArray(); // if roles loaded
            //var jwtId = Guid.NewGuid().ToString("N");

            //var accessToken = jwtTokenGenerator.GenerateAccessToken(user, jwtId, roles);

            //var refreshToken = refreshTokenFactory.GenerateRefreshToken();
            //var refreshTokenHash = refreshTokenFactory.HashRefreshToken(refreshToken);

            //var entity = new RefreshToken
            //{
            //    UserId = user.Id,
            //    TenantId = user.TenantId,
            //    TokenHash = refreshTokenHash,
            //    JwtId = jwtId,
            //    ClientId = clientId,
            //    DeviceId = deviceId,
            //    DeviceName = deviceName,
            //    IpAddress = ipAddress,
            //    UserAgent = userAgent,
            //    ExpiresAtUtc = DateTimeOffset.UtcNow.AddDays(_jwtOptions.RefreshTokenLifetimeDays)
            //};

            //dbContext.RefreshTokens.Add(entity);
            var accessToken = "";
            var refreshToken = "";
            return new TokenPair(
                accessToken,
                refreshToken,
                _jwtOptions.AccessTokenLifetimeMinutes * 60);
        }
    }
}
