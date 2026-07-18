namespace Serveo.Application.Abstractions
{
    public interface IRefreshTokenFactory
    {
        string GenerateRefreshToken(int size = 64);
        string HashRefreshToken(string refreshToken);
    }
}
