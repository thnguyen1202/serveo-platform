namespace Serveo.WebApi.Models.Auth
{
    public sealed record LoginResponse(string AccessToken, string RefreshToken, TimeSpan ExpiresIn);
}
