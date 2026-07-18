using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Serveo.Application.Abstractions.Messaging;
using Serveo.Application.Services.Administration.Accounts.Dto;
using Serveo.Domain.AppSettings;
using Serveo.Domain.Entities.Identity;
using Serveo.Domain.Interfaces;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Serveo.Application.Services.Administration.Accounts
{
    public class JwtService(
        IUnitOfWork unitOfWork,
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        IOptions<JwtOptions> jwt) : IJwtService, IService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        private readonly UserManager<User> _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        private readonly SignInManager<User> _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
        private readonly JwtOptions _jwt = jwt.Value;

        public async Task<AuthTokenResult> AuthTokenAsync(User user, string? ip)
        {
            var accessToken = await GenerateAccessTokenAsync(user);
            var (token, hash) = GenerateRefreshToken();

            await CreateRefreshTokenAsync(user.Id, hash, ip); // save RefreshToken data

            return new AuthTokenResult(accessToken, token, _jwt.ExpireMinutes, "", "");
        }

        public async Task<ICommandResult<AuthTokenResult>> RefreshToken(string refreshToken, string? ip = null)
        {
            var tokenHash = Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(refreshToken)));
            var token = await _unitOfWork.Set<RefreshToken>().SingleOrDefaultAsync(x => x.TokenHash == tokenHash);


            if (token == null)
                return (ICommandResult<AuthTokenResult>)CommandResult.Failure(new CommandError { Message = "Unauthorized" });

            if (!token.IsActive)
                return (ICommandResult<AuthTokenResult>)CommandResult.Failure(new CommandError { Message = "Unauthorized" });

            var user = await _userManager.FindByIdAsync(token.UserId.ToString());
            var newAccessToken = await GenerateAccessTokenAsync(user!);
            var (newRefreshToken, newRefreshTokenHash) = GenerateRefreshToken();

            token.RevokedAt = DateTime.UtcNow;
            token.RevokedByIp = ip;
            token.ReplacedByTokenHash = newRefreshTokenHash;

            await CreateRefreshTokenAsync(user!.Id, newRefreshTokenHash, ip); // save RefreshToken data

            return CommandResult<AuthTokenResult>.Success(new AuthTokenResult(newAccessToken, newRefreshToken, _jwt.ExpireMinutes, "", ""));
        }



        public async Task<string> GenerateAccessTokenAsync(User user)
        {
            //var claims = new List<Claim>
            //{
            //    new Claim("userId", userId),
            //    new Claim(ClaimTypes.Email, email),
            //    new Claim(ClaimTypes.Role, role)
            //};
            var claims = (await _signInManager.CreateUserPrincipalAsync(user)).Claims.Where(x => x.Type != "Permission").ToList();
            if (!string.IsNullOrEmpty(user.DisplayName)) claims.Add(new Claim("DisplayName", user.DisplayName));
            if (!string.IsNullOrEmpty(user.TimeZoneId)) claims.Add(new Claim("TimeZoneId", user.TimeZoneId));
            if (user.TenantId.HasValue)
            {
                claims.Add(new Claim("TenantId", user.TenantId.Value.ToString()));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwt.ExpireMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task CreateRefreshTokenAsync(Guid userId, string refreshTokenHash, string? createdByIp)
        {
            _unitOfWork.Set<RefreshToken>().Add(new RefreshToken
            {
                UserId = userId,
                TokenHash = refreshTokenHash,
                CreatedByIp = createdByIp,
                ExpiresAt = DateTime.UtcNow.AddDays(30)
            });
            await _unitOfWork.SaveChangesAsync();
        }

        public (string token, string hash) GenerateRefreshToken(int size = 64)
        {
            var bytes = RandomNumberGenerator.GetBytes(size);

            var token = WebEncoders.Base64UrlEncode(bytes);

            var hash = Convert.ToHexString(
                SHA256.HashData(Encoding.UTF8.GetBytes(token))
            );

            return (token, hash);
        }
    }
}
