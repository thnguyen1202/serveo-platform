using System.Runtime.InteropServices;
using System.Text;

namespace Serveo.Domain.Helpers
{
    public static class Base62
    {
        private const string Alphabet = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

        public static string Encode(ReadOnlySpan<byte> data)
        {
            if (data.Length == 0) return string.Empty;

            var bytes = new byte[data.Length];
            data.CopyTo(bytes);

            var sb = new StringBuilder();
            while (true)
            {
                int remainder = 0;
                for (int i = 0; i < bytes.Length; i++)
                {
                    int value = (remainder << 8) + bytes[i];
                    bytes[i] = (byte)(value / 62);
                    remainder = value % 62;
                }

                sb.Insert(0, Alphabet[remainder]);

                int leadingZero = 0;
                while (leadingZero < bytes.Length && bytes[leadingZero] == 0) leadingZero++;
                if (leadingZero == bytes.Length) break;

                bytes = bytes.AsSpan(leadingZero).ToArray();
            }

            return sb.ToString();
        }

        public static void Decode(ReadOnlySpan<char> input, Span<byte> output)
        {
            output.Clear();

            foreach (var c in input)
            {
                int carry = Alphabet.IndexOf(c);
                if (carry < 0) throw new InvalidOperationException("Invalid Base62 char");

                for (int i = output.Length - 1; i >= 0; i--)
                {
                    carry += output[i] * 62;
                    output[i] = (byte)(carry & 0xFF);
                    carry >>= 8;
                }
            }
        }

        public static string EncodeGuid(Guid guid)
        {
            Span<byte> bytes = stackalloc byte[16];
            guid.TryWriteBytes(bytes);

            // convert to BigInteger
            var value = new System.Numerics.BigInteger(bytes, isUnsigned: true, isBigEndian: true);

            Span<char> buffer = stackalloc char[22];

            int pos = buffer.Length;

            while (value > 0)
            {
                value = System.Numerics.BigInteger.DivRem(value, 62, out var remainder);
                buffer[--pos] = Alphabet[(int)remainder];
            }

            return new string(buffer[pos..]);
        }

        public static Guid DecodeGuid(string base62)
        {
            System.Numerics.BigInteger result = 0;

            foreach (var c in base62)
            {
                result *= 62;
                result += Alphabet.IndexOf(c);
            }

            var bytes = result.ToByteArray(isUnsigned: true, isBigEndian: true);

            if (bytes.Length < 16)
            {
                var padded = new byte[16];
                Buffer.BlockCopy(bytes, 0, padded, 16 - bytes.Length, bytes.Length);
                bytes = padded;
            }

            return new Guid(bytes);
        }

        public static ReadOnlySpan<byte> LongToSpan(long input)
        {
            // 1. Create a Span around the reference of the long variable
            Span<long> longSpan = MemoryMarshal.CreateSpan(ref input, 1);

            // 2. Cast the long span into a byte span (8 bytes long)
            ReadOnlySpan<byte> byteSpan = MemoryMarshal.Cast<long, byte>(longSpan);

            return byteSpan;
        }
    }
}
