using Serveo.Domain.Entities.Base;
using Serveo.Domain.Helpers;
using System.Security.Cryptography;
using System.Text;

namespace Serveo.Domain.Entities.Authorization
{
    public sealed class ApiClientKey : CreationAuditableEntity
    {
        public Guid ClientId { get; set; }
        public string KeyHash { get; set; } = default!;
        public ApiClientKeyStatus Status { get; set; } = ApiClientKeyStatus.Active;
        public DateTimeOffset? ExpiresAt { get; set; }
        public DateTimeOffset? RevokedAt { get; set; }

        // Theo dõi lần sử dụng cuối để phát hiện key không còn dùng hoặc có dấu hiệu bất thường.
        public DateTimeOffset? LastUsedAt { get; set; }
        public string? LastUsedIp { get; set; }


        public ApiClient Client { get; set; } = default!;

        public static (string key, string keyHash) GenerateKey(string type = "live")
        {
            var key = $"sk-{type}-{Base62.EncodeGuid(Guid.CreateVersion7())}-{Base62.Encode(Base62.LongToSpan(DateTime.UtcNow.Ticks))}";
            var keyHash = Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(key)));

            return (key, keyHash);
        }
    }

    public enum ApiClientKeyStatus
    {
        Active = 1,
        Inactive,
        Revoked,
        Expired
    }
}
