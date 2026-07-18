namespace Serveo.Application.Services.Administration.Accounts.Dto
{
    public record IssueJwtResult(string AccessToken, string RefreshToken, int ExpiresIn);
}
