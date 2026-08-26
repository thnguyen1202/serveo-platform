using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Serveo.Application.Abstractions;
using Serveo.Domain.Entities.Identity;
using Serveo.Infrastructure.Authentication.Jwt;

namespace Serveo.Infrastructure.Authentication.Tokens
{
    public sealed class TokenService(
        IUnitOfWork unitOfWork,
        SignInManager<User> signInManager,
        IJwtTokenGenerator jwtTokenGenerator,
        IRefreshTokenFactory refreshTokenFactory,
        IOptions<JwtOptions> jwtOptions) : ITokenService
    {
        private readonly JwtOptions _jwtOptions = jwtOptions.Value;

        public string HashToken(string token)
        {
            return refreshTokenFactory.HashRefreshToken(token);
        }

        public async Task<TokenPair> CreateTokenPairAsync(
            User user,
            ClientType clientType,
            string? deviceId,
            string? ipAddress,
            string? userAgent,
            bool isRemember,
            CancellationToken ct)
        {
            var principal = await signInManager.CreateUserPrincipalAsync(user);
            var accessToken = jwtTokenGenerator.GenerateAccessToken(user, principal);

            var refreshToken = refreshTokenFactory.GenerateRefreshToken();
            var refreshTokenHash = refreshTokenFactory.HashRefreshToken(refreshToken);

            await CreateUserSessionAsync(
                user.Id,
                clientType,
                deviceId ?? string.Empty,
                ipAddress ?? string.Empty,
                userAgent ?? string.Empty,
                refreshTokenHash,
                isRemember ? 90 : _jwtOptions.RefreshTokenLifetimeDays,
                ct);

            return new TokenPair(
                accessToken,
                refreshToken,
                _jwtOptions.AccessTokenLifetimeMinutes * 60);
        }

        public async Task<TokenPair> RefreshTokenPairAsync(
            User user,
            UserSession userSession,
            string? ipAddress,
            string? userAgent,
            CancellationToken ct)
        {
            var principal = await signInManager.CreateUserPrincipalAsync(user);
            var accessToken = jwtTokenGenerator.GenerateAccessToken(user, principal);

            var refreshToken = refreshTokenFactory.GenerateRefreshToken();
            var refreshTokenHash = refreshTokenFactory.HashRefreshToken(refreshToken);

            userSession.Refresh(
                refreshTokenHash: refreshTokenHash,
                ipAddress: ipAddress ?? string.Empty,
                userAgent: userAgent ?? string.Empty,
                now: DateTimeOffset.UtcNow,
                refreshLifetimeDays: _jwtOptions.RefreshTokenLifetimeDays);

            unitOfWork.UserSessions.Update(userSession);
            await unitOfWork.SaveChangesAsync(ct);

            return new TokenPair(
                accessToken,
                refreshToken,
                _jwtOptions.AccessTokenLifetimeMinutes * 60);
        }

        private async Task CreateUserSessionAsync(
            Guid userId,
            ClientType clientType,
            string deviceId,
            string ipAddress,
            string userAgent,
            string refreshTokenHash,
            int absoluteLifetimeDays,
            CancellationToken ct
            )
        {
            var sessions = await unitOfWork.UserSessions.Query()
                .Where(x => x.UserId == userId && x.ClientType == clientType)
                .OrderBy(x => x.LastActivityAt)
                .ToListAsync(ct);

            var isUpdate = true;
            var session = sessions.FirstOrDefault(x => x.DeviceId == deviceId);
            if (session is null)
            {
                if (sessions.Count < _jwtOptions.MaxSessionsPerClientType)
                {
                    session = new UserSession
                    {
                        UserId = userId,
                        ClientType = clientType
                    };
                    isUpdate = false;
                }
                else
                {
                    session = sessions[0];
                    isUpdate = true;
                }
            }

            var now = DateTimeOffset.UtcNow;
            session.Replace(
                deviceId,
                refreshTokenHash,
                ipAddress,
                userAgent,
                now,
                _jwtOptions.RefreshTokenLifetimeDays,
                absoluteLifetimeDays);

            if (isUpdate)
                unitOfWork.UserSessions.Update(session);
            else
                unitOfWork.UserSessions.Add(session);

            await unitOfWork.SaveChangesAsync(ct);
        }
    }
}
