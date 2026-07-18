using Microsoft.AspNetCore.WebUtilities;
using Microsoft.IdentityModel.Tokens;
using Serveo.Infrastructure.Authentication.Jwt;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Serveo.Application.Features.Identity
{
    internal class JwtHelper
    {
        public static string GenerateAccessToken(List<Claim> claims, JwtOptions jwt)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.SigningKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: jwt.Issuer,
                audience: jwt.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(jwt.AccessTokenLifetimeMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public static (string refreshToken, string refreshTokenHash) GenerateRefreshToken(int size = 64)
        {
            var bytes = RandomNumberGenerator.GetBytes(size);
            var token = WebEncoders.Base64UrlEncode(bytes);
            var tokenHash = Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(token)));

            return (token, tokenHash);
        }
    }
}
