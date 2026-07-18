using Microsoft.AspNetCore.Identity;
using Serveo.Application.Abstractions;
using Serveo.Domain.Entities.Identity;

namespace Serveo.Infrastructure.Authentication.Passwords
{
    public sealed class PasswordHasherService : IPasswordHasherService
    {
        private readonly PasswordHasher<User> _hasher = new();

        public string Hash(string password)
            => _hasher.HashPassword(new User(), password);

        public bool Verify(string hashedPassword, string providedPassword)
        {
            var result = _hasher.VerifyHashedPassword(new User(), hashedPassword, providedPassword);
            return result is PasswordVerificationResult.Success
                or PasswordVerificationResult.SuccessRehashNeeded;
        }
    }
}
