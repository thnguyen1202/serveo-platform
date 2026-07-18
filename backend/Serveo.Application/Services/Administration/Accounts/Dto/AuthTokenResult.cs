namespace Serveo.Application.Services.Administration.Accounts.Dto
{
    public record AuthTokenResult(
        string AccessToken,
        string RefreshToken,
        int ExpiresInSeconds,
        string TokenType,
        string Scope
    )
    {
        public TimeSpan ExpiresIn => TimeSpan.FromSeconds(ExpiresInSeconds);
    }
}
