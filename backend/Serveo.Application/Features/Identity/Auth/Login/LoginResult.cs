namespace Serveo.Application.Features.Identity.Auth.Login
{
    public record LoginResult(
        string AccessToken,
        string RefreshToken,
        int ExpiresInSeconds,
        string? TokenType = "Bearer",
        string? Scope = null
    )
    {
        public int ExpiresIn => ExpiresInSeconds;
    }
}
