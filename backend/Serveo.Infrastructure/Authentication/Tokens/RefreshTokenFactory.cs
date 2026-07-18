using Microsoft.AspNetCore.WebUtilities;
using Serveo.Application.Abstractions;
using System.Security.Cryptography;
using System.Text;

namespace Serveo.Infrastructure.Authentication.Tokens
{
    public sealed class RefreshTokenFactory : IRefreshTokenFactory
    {
        public string GenerateRefreshToken(int size = 64)
        {
            var bytes = RandomNumberGenerator.GetBytes(size);
            return WebEncoders.Base64UrlEncode(bytes);
        }

        public string HashRefreshToken(string refreshToken)
        {
            var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(refreshToken));
            return Convert.ToHexString(bytes);
        }
    }
}
