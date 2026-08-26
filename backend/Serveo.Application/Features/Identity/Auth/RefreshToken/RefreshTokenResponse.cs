namespace Serveo.Application.Features.Identity.Auth.RefreshToken
{
    public record RefreshTokenResult(
        string AccessToken,
        string RefreshToken,
        int ExpiresInSeconds,
        string? TokenType = null,
        string? Scope = null,
        bool IsRemember = false
    )
    {
        public int ExpiresIn => ExpiresInSeconds;
    }
}
