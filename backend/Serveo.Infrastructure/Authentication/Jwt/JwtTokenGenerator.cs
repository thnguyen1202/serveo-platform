using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Serveo.Application.Abstractions;
using Serveo.Domain;
using Serveo.Domain.Entities.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Serveo.Infrastructure.Authentication.Jwt
{
    public sealed class JwtTokenGenerator(IOptions<JwtOptions> options) : IJwtTokenGenerator
    {
        private readonly JwtOptions _jwtOptions = options.Value;

        public string GenerateAccessToken(List<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SigningKey));
            var token = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtOptions.AccessTokenLifetimeMinutes),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerateAccessToken(User user, ClaimsPrincipal principal)
        {
            var claims = GetClaims(user, principal);

            return GenerateAccessToken(claims);
        }

        private static List<Claim> GetClaims(User user, ClaimsPrincipal principal)
        {
            //var claims = new List<Claim>
            //{
            //    new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            //    new(JwtRegisteredClaimNames.Jti, jwtId),
            //    new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            //    new(ClaimTypes.Name, user.UserName),
            //    new(ClaimTypes.Email, user.Email),
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
    }
}
