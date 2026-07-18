using System.Buffers.Text;
using System.Security.Cryptography;

namespace Serveo.SharedKernel
{
    public class PublicTokenGenerator
    {
        public static string Generate(int size = 64)
        {
            Span<byte> bytes = stackalloc byte[size];

            RandomNumberGenerator.Fill(bytes);

            return Base64Url.EncodeToString(bytes);
        }
    }
}
