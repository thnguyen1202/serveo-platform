namespace Serveo.Infrastructure.Authentication.Jwt
{
    public sealed class JwtOptions
    {
        public const string SectionName = "Authentication:Schemes:Jwt";

        public string SigningKey { get; init; } = default!;
        public string Issuer { get; init; } = default!;
        public string Audience { get; init; } = default!;

        public int AccessTokenLifetimeMinutes { get; init; } = 15;
        public int RefreshTokenLifetimeDays { get; init; } = 30;
    }
}
