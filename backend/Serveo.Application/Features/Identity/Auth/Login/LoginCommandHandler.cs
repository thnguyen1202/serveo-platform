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

namespace Serveo.Application.Features.Identity.Auth.Login
{
    public sealed class LoginCommandHandler(
        IUnitOfWork unitOfWork,
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        ITokenService tokenService,
        IOptions<JwtOptions> jwtOptions
    ) : ICommandHandler<LoginCommand, ICommandResult<LoginResult>>
    {
        private readonly JwtOptions _jwtOptions = jwtOptions.Value;

        public async Task<ICommandResult<LoginResult>> HandleAsync(LoginCommand request, CancellationToken ct)
        {
            var user = await userManager.FindByEmailAsync(request.Email);

            if (user is null)
                return CommandResult<LoginResult>.Failure(
                    CommandErrors.Unauthorized(ErrorCodes.Auth.InvalidCredentials, "Invalid credentials."));

            if (request.ClientType == ClientType.Admin && user.TenantId.HasValue ||
                request.ClientType == ClientType.Ops && !user.TenantId.HasValue)
                return CommandResult<LoginResult>.Failure(
                    CommandErrors.Unauthorized(ErrorCodes.Auth.InvalidCredentials, "Invalid credentials."));

            if (!user.EmailConfirmed)
                return CommandResult<LoginResult>.Failure(
                    CommandErrors.Unauthorized(ErrorCodes.Auth.InvalidConfirmed, "User not confirmed yet."));

            var lockoutOnFailure = true;
            var signinResult = await signInManager.CheckPasswordSignInAsync(user, request.Password, lockoutOnFailure);
            if (signinResult.Succeeded)
            {
                var tokenPair = await tokenService.CreateTokenPairAsync(
                    user,
                    request.ClientType,
                    request.DeviceId,
                    request.IpAddress,
                    request.UserAgent,
                    request.IsRemember,
                    ct);

                var loginResult = new LoginResult(
                    tokenPair.AccessToken,
                    tokenPair.RefreshToken,
                    tokenPair.ExpiresInSeconds);

                return CommandResult<LoginResult>.Success(loginResult);
            }

            if (signinResult.IsLockedOut)
            {
                return CommandResult<LoginResult>.Failure(
                    CommandErrors.Unauthorized(ErrorCodes.Auth.UserLockedOut, "Account is temporarily locked."));
            }
            else
            {
                var message = "Invalid login attempt.";
                string remaining = "";
                if (lockoutOnFailure)
                {
                    var attemptsLeft = userManager.Options.Lockout.MaxFailedAccessAttempts - await userManager.GetAccessFailedCountAsync(user);
                    remaining = $"Remaining attempts : {attemptsLeft}.";
                }
                if (!string.IsNullOrWhiteSpace(remaining))
                {
                    message += $" {remaining}";
                }

                return CommandResult<LoginResult>.Failure(CommandErrors.Unauthorized(ErrorCodes.Auth.InvalidLoginAttempt, message));
            }
        }

        private static List<Claim> GetClaims(User user, ClaimsPrincipal principal)
        {
            //var claims = new List<Claim>
            //{
            //    new Claim("userId", userId),
            //    new Claim(ClaimTypes.Email, email),
            //    new Claim(ClaimTypes.Role, role)
            //};
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

        //private async Task<string> GenerateAccessTokenAsync(User user)
        //{
        //    //var claims = new List<Claim>
        //    //{
        //    //    new Claim("userId", userId),
        //    //    new Claim(ClaimTypes.Email, email),
        //    //    new Claim(ClaimTypes.Role, role)
        //    //};
        //    var claims = (await _signInManager.CreateUserPrincipalAsync(user)).Claims.Where(x => x.Type != "Permission").ToList();
        //    if (!string.IsNullOrEmpty(user.DisplayName)) claims.Add(new Claim("DisplayName", user.DisplayName));
        //    if (!string.IsNullOrEmpty(user.TimeZoneId)) claims.Add(new Claim("TimeZoneId", user.TimeZoneId));
        //    if (user.TenantId.HasValue)
        //    {
        //        claims.Add(new Claim("TenantId", user.TenantId.Value.ToString()));
        //    }

        //    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
        //    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        //    var token = new JwtSecurityToken(
        //        issuer: _jwt.Issuer,
        //        audience: _jwt.Audience,
        //        claims: claims,
        //        expires: DateTime.UtcNow.AddMinutes(_jwt.ExpireMinutes),
        //        signingCredentials: creds
        //    );

        //    return new JwtSecurityTokenHandler().WriteToken(token);
        //}

        private async Task CreateRefreshTokenAsync(Guid userId, string refreshTokenHash, string? createdByIp)
        {
            unitOfWork.RefreshTokens.Add(new Domain.Entities.Identity.RefreshToken
            {
                UserId = userId,
                TokenHash = refreshTokenHash,
                CreatedByIp = createdByIp,
                ExpiresAt = DateTime.UtcNow.AddDays(_jwtOptions.RefreshTokenLifetimeDays)
            });

            await unitOfWork.SaveChangesAsync();
        }

        private async Task CreateUserSessionAsync(
            Guid userId,
            string refreshTokenHash,
            LoginCommand request)
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
            var absoluteLifetime = request.IsRemember ? 90 : _jwtOptions.RefreshTokenLifetimeDays;

            session.Replace(
                request.DeviceId,
                refreshTokenHash,
                //request.DeviceName,
                //request.Browser,
                //request.OperatingSystem,
                request.IpAddress,
                request.UserAgent,
                now,
                _jwtOptions.RefreshTokenLifetimeDays,
                absoluteLifetime);

            await unitOfWork.SaveChangesAsync();
        }
    }
}
