using Serveo.Domain.Entities.Base;

namespace Serveo.Domain.Entities.Identity
{
    public sealed class UserSession : CreationAuditableEntity, IMustHaveUser
    {
        public Guid UserId { get; set; }
        public ClientType ClientType { get; set; }
        public string DeviceId { get; set; } = default!;

        //public string? DeviceName { get; set; }
        //public string Browser { get; set; } = default!;
        //public string OperatingSystem { get; set; } = default!;
        public string IpAddress { get; set; } = default!;
        public string? UserAgent { get; set; }
        public string RefreshTokenHash { get; set; } = default!;
        public string? PreviousTokenHash { get; set; }
        public DateTimeOffset LastActivityAt { get; set; }

        /// <summary>
        /// Absolute lifetime
        /// </summary>
        public DateTimeOffset? AbsoluteExpiresAt { get; set; }  // Giới hạn tối đa của phiên (tùy chọn)
        public DateTimeOffset? RevokedAt { get; set; }
        public DateTimeOffset ExpiresAt { get; set; } // Refresh token hết hạn


        public bool IsExpired => DateTime.UtcNow >= ExpiresAt || ExpiresAt >= AbsoluteExpiresAt;
        public bool IsRevoked => RevokedAt != null;
        public bool IsActive => !IsExpired && !IsRevoked;



        //public void Login(
        //    string refreshTokenHash,
        //    string? deviceName,
        //    string? browser,
        //    string? operatingSystem,
        //    string? ipAddress,
        //    string? userAgent,
        //    DateTimeOffset now,
        //    int refreshLifetimeDays,
        //    int absoluteLifetimeDays)
        //{
        //    PreviousTokenHash = null;
        //    RefreshTokenHash = refreshTokenHash;

        //    DeviceName = deviceName;
        //    Browser = browser ?? "";
        //    OperatingSystem = operatingSystem ?? "";
        //    IpAddress = ipAddress ?? "";
        //    UserAgent = userAgent;

        //    LastActivityAt = now;
        //    ExpiresAt = now.AddDays(refreshLifetimeDays);
        //    AbsoluteExpiresAt = now.AddDays(absoluteLifetimeDays);
        //    IsCurrent = true;
        //}

        public void Replace(
            string deviceId,
            string refreshTokenHash,
            //string? deviceName,
            //string? browser,
            //string? operatingSystem,
            string ipAddress,
            string userAgent,
            DateTimeOffset now,
            int refreshLifetimeDays,
            int absoluteLifetimeDays,
            bool isRemember = false)
        {
            DeviceId = deviceId;
            PreviousTokenHash = null;
            RefreshTokenHash = refreshTokenHash;

            //DeviceName = deviceName;
            //Browser = browser ?? "";
            //OperatingSystem = operatingSystem ?? "";
            IpAddress = ipAddress;
            UserAgent = userAgent;

            LastActivityAt = now;
            ExpiresAt = now.AddDays(refreshLifetimeDays);
            AbsoluteExpiresAt = now.AddDays(absoluteLifetimeDays);
            if (isRemember) AbsoluteExpiresAt = null;
        }

        public void Refresh(
            string refreshTokenHash,
            //string? deviceName,
            //string? browser,
            //string? operatingSystem,
            string ipAddress,
            string userAgent,
            DateTimeOffset now,
            int refreshLifetimeDays)
        {
            PreviousTokenHash = RefreshTokenHash;
            RefreshTokenHash = refreshTokenHash;

            //DeviceName = deviceName;
            //Browser = browser ?? "";
            //OperatingSystem = operatingSystem ?? "";
            IpAddress = ipAddress;
            UserAgent = userAgent;

            LastActivityAt = now;
            ExpiresAt = now.AddDays(refreshLifetimeDays);
        }
    }

    public enum ClientType
    {
        Admin,
        Ops,
        Pos,
        QrPwa,
        MobileApp
    }
}
