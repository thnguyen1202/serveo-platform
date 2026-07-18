namespace Serveo.Application.Features.Identity.Auth.Login
{
    public sealed record LoginResult(
        string AccessToken,
        string RefreshToken,
        int ExpiresInSeconds,
        string? TokenType = null,
        string? Scope = null
    )
    {
        public TimeSpan ExpiresIn => TimeSpan.FromSeconds(ExpiresInSeconds);
    }
}
