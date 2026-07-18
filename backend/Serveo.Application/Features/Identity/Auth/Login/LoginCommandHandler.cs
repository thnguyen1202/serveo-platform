using Microsoft.AspNetCore.Identity;
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
        IOptions<JwtOptions> jwtOptions
    //ILoginApiKeyGuard apiKeyGuard,
    //IRequestContext requestContext,
    //IMapper mapper,
    //ILogger<LoginCommandHandler> logger
    ) : ICommandHandler<LoginCommand, ICommandResult<LoginResult>>
    {
        private readonly JwtOptions _jwtOptions = jwtOptions.Value;

        public async Task<ICommandResult<LoginResult>> HandleAsync(LoginCommand request, CancellationToken ct)
        {
            var user = await userManager.FindByEmailAsync(request.Email);

            if (user is null)
                return CommandResult<LoginResult>.Failure(
                    CommandErrors.Unauthorized(ErrorCodes.Auth.InvalidCredentials, "Invalid credentials."));

            if (!user.EmailConfirmed)
                return CommandResult<LoginResult>.Failure(
                    CommandErrors.Unauthorized(ErrorCodes.Auth.InvalidConfirmed, "User not confirmed yet."));

            var lockoutOnFailure = true;
            var signinResult = await signInManager.CheckPasswordSignInAsync(user, request.Password, lockoutOnFailure);
            if (signinResult.Succeeded)
            {
                var principal = await signInManager.CreateUserPrincipalAsync(user);
                var claims = GetClaims(user, principal);
                var accessToken = JwtHelper.GenerateAccessToken(claims, _jwtOptions);
                var (refreshToken, refreshTokenHash) = JwtHelper.GenerateRefreshToken();
                var loginResult = new LoginResult(accessToken, refreshToken, _jwtOptions.AccessTokenLifetimeMinutes * 60);
                await CreateRefreshTokenAsync(user.Id, refreshTokenHash, request.Ip);

                return CommandResult<LoginResult>.Success(loginResult);
            }

            if (signinResult.IsLockedOut)
            {
                return CommandResult<LoginResult>.Failure(
                    CommandErrors.Unauthorized(ErrorCodes.Auth.UserLockedOut, "Account is temporarily locked."));
            }
            else
            {
                var errors = new List<CommandError>()
                {
                    CommandErrors.Unauthorized( ErrorCodes.Auth.InvalidLoginAttempt, "Invalid login attempt.")
                };
                string remaining = "";
                if (lockoutOnFailure)
                {
                    var attemptsLeft = userManager.Options.Lockout.MaxFailedAccessAttempts - await userManager.GetAccessFailedCountAsync(user);
                    remaining = $"Remaining attempts : {attemptsLeft}.";
                }
                if (!string.IsNullOrWhiteSpace(remaining))
                {
                    errors.Add(CommandErrors.Unauthorized("REMAINING_ATTEMPTS", remaining));
                }

                return CommandResult<LoginResult>.Failure(errors);
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
            //_ = unitOfWork.Set<RefreshToken>().Add(new Domain.Entities.Identity.RefreshToken
            //{
            //    UserId = userId,
            //    TokenHash = refreshTokenHash,
            //    CreatedByIp = createdByIp,
            //    ExpiresAt = DateTime.UtcNow.AddDays(30)
            //});
            await unitOfWork.SaveChangesAsync();
        }
    }
}
