using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Serveo.Application.Abstractions;
using Serveo.Application.Abstractions.Mediator;
using Serveo.Application.Common.Results;
using Serveo.Application.Services;
using Serveo.Domain;
using Serveo.Domain.Entities.Identity;
using Serveo.Infrastructure.Authentication.Jwt;
using System.Security.Claims;

namespace Serveo.Application.Features.Identity.Auth.RefreshToken
{
    public sealed class RefreshTokenHandler(
        IUnitOfWork unitOfWork,
        UserManager<User> userManager,
        ITokenService tokenService,
        IOptions<JwtOptions> jwtOptions
    ) : ICommandHandler<RefreshTokenCommand, ICommandResult<RefreshTokenResult>>
    {
        private readonly JwtOptions _jwtOptions = jwtOptions.Value;

        public async Task<ICommandResult<RefreshTokenResult>> HandleAsync(RefreshTokenCommand request, CancellationToken ct)
        {
            var refreshTokenHash = tokenService.HashToken(request.RefreshToken);
            var refreshToken = await unitOfWork.UserSessions.FindAsync(x => x.RefreshTokenHash == refreshTokenHash, ct);

            if (refreshToken is null)
                return CommandResult<RefreshTokenResult>.Failure(
                    CommandErrors.Unauthorized(ErrorCodes.Auth.InvalidRefreshToken, "Invalid refresh token."));

            if (refreshToken.DeviceId != request.DeviceId || refreshToken.ClientType != request.ClientType)
                return CommandResult<RefreshTokenResult>.Failure(
                    CommandErrors.Unauthorized(ErrorCodes.Auth.InvalidRefreshToken, "Invalid refresh token."));

            if (refreshToken.IsRevoked)
                return CommandResult<RefreshTokenResult>.Failure(
                    CommandErrors.Unauthorized(ErrorCodes.Auth.RefreshTokenRevoked, "Refresh token has been revoked."));

            if (refreshToken.IsExpired)
                return CommandResult<RefreshTokenResult>.Failure(
                    CommandErrors.Unauthorized(ErrorCodes.Auth.RefreshTokenExpired, "Refresh token has expired."));

            var user = await userManager.FindByIdAsync(refreshToken.UserId.ToString());

            if (user is null || user.Status != UserStatus.Active)
                return CommandResult<RefreshTokenResult>.Failure(
                    CommandErrors.Unauthorized(ErrorCodes.Auth.InvalidRefreshToken, "Invalid refresh token."));

            if (request.ClientType == ClientType.Admin && user.TenantId.HasValue ||
                request.ClientType == ClientType.Ops && !user.TenantId.HasValue)
                return CommandResult<RefreshTokenResult>.Failure(
                    CommandErrors.Unauthorized(ErrorCodes.Auth.InvalidRefreshToken, "Invalid refresh token."));

            var tokenPair = await tokenService.RefreshTokenPairAsync(user, refreshToken, request.IpAddress, request.UserAgent, ct);

            return CommandResult<RefreshTokenResult>.Success(new RefreshTokenResult(
                tokenPair.AccessToken,
                tokenPair.RefreshToken,
                tokenPair.ExpiresInSeconds,
                IsRemember: !refreshToken.AbsoluteExpiresAt.HasValue));
        }

        private static List<Claim> GetClaims(User user, ClaimsPrincipal principal)
        {
            var claims = principal.Claims.Where(x => x.Type != CustomClaimTypes.Permission).ToList();
            if (!string.IsNullOrEmpty(user.DisplayName)) claims.Add(new Claim(CustomClaimTypes.DisplayName, user.DisplayName));
            if (!string.IsNullOrEmpty(user.TimeZoneId)) claims.Add(new Claim(CustomClaimTypes.TimeZoneId, user.TimeZoneId));
            if (user.TenantId.HasValue)
            {
                claims.Add(new Claim(CustomClaimTypes.TenantId, user.TenantId.Value.ToString()));
            }
            if (user.BusinessId.HasValue)
            {
                claims.Add(new Claim(CustomClaimTypes.BusinessId, user.BusinessId.Value.ToString()));
            }
            if (user.BranchId.HasValue)
            {
                claims.Add(new Claim(CustomClaimTypes.BranchId, user.BranchId.Value.ToString()));
            }

            return claims;
        }

        private async Task RefreshUserSessionAsync(
            Guid userId,
            string refreshTokenHash,
            RefreshTokenCommand request)
        {
            var sessions = await unitOfWork.UserSessions.Query()
                .Where(x => x.UserId == userId && x.ClientType == request.ClientType)
                .OrderBy(x => x.LastActivityAt)
                .ToListAsync();

            var session = sessions.FirstOrDefault(x => x.DeviceId == request.DeviceId);
            if (session is null)
            {
                if (sessions.Count < _jwtOptions.MaxSessionsPerClientType)
                {

                    session = new UserSession
                    {
                        UserId = userId,
                        ClientType = request.ClientType
                    };

                    unitOfWork.UserSessions.Add(session);
                }
                else
                {
                    session = sessions[0];
                }
            }

            var now = DateTimeOffset.UtcNow;

            session.Refresh(
                refreshTokenHash,
                //request.DeviceName,
                //request.Browser,
                //request.OperatingSystem,
                request.IpAddress,
                request.UserAgent,
                now,
                _jwtOptions.RefreshTokenLifetimeDays);

            await unitOfWork.SaveChangesAsync();
        }
    }
}
