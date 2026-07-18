namespace Serveo.Application.Abstractions
{
    public interface ITokenService
    {
    }

    public sealed record TokenPair(
        string AccessToken,
        string RefreshToken,
        int ExpiresInSeconds
    );
}
