namespace Serveo.Domain.Entities.Base
{
    public static class DbLengths
    {
        public const int ShortCode = 16;
        public const int Code = 32;
        public const int LongCode = 64;

        public const int Name = 128;
        public const int LongName = 256;

        public const int Email = 256;

        public const int ShortText = 512;
        public const int NormalText = 1024;
        public const int LongText = 2048;

        public const int Url = 2048;

        public const int JwtToken = 4096;

        public const int RefreshToken = 256;
    }
}
