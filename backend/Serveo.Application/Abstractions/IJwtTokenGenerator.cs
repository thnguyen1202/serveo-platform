using Serveo.Domain.Entities.Identity;
using System.Security.Claims;

namespace Serveo.Application.Abstractions
{
    public interface IJwtTokenGenerator
    {
        string GenerateAccessToken(User user, ClaimsPrincipal principal);
        string GenerateAccessToken(List<Claim> claims);
    }
}
