namespace Serveo.WebApi.Models.Auth
{
    public sealed record RefreshTokenResponse(
        string AccessToken
    //string RefreshToken, 
    //int ExpiresIn
    );
}
