namespace Serveo.Application.Common.Results
{
    public static class ErrorCodes
    {
        public static class General
        {
            public const string Unknown = "UNKNOWN";
            public const string InternalServerError = "INTERNAL_SERVER_ERROR";
            public const string InvalidRequest = "INVALID_REQUEST";
            public const string ConcurrencyConflict = "CONCURRENCY_CONFLICT";
        }

        public static class Auth
        {
            public const string InvalidCredentials = "INVALID_CREDENTIALS";
            public const string InvalidAccessToken = "INVALID_ACCESS_TOKEN";
            public const string InvalidRefreshToken = "INVALID_REFRESH_TOKEN";
            public const string RefreshTokenExpired = "REFRESH_TOKEN_EXPIRED";
            public const string RefreshTokenRevoked = "REFRESH_TOKEN_REVOKED";
            public const string UserLockedOut = "USER_LOCKED_OUT";
            public const string UserInactive = "USER_INACTIVE";
            public const string PermissionDenied = "PERMISSION_DENIED";
            public const string InvalidLoginAttempt = "INVALID_LOGIN_ATTEMPT";
            public const string InvalidConfirmed = "INVALID_CONFIRMED";
        }

        public static class Tenant
        {
            public const string NotFound = "TENANT_NOT_FOUND";
            public const string Inactive = "TENANT_INACTIVE";
            public const string Suspended = "TENANT_SUSPENDED";
            public const string SubscriptionExpired = "TENANT_SUBSCRIPTION_EXPIRED";
            public const string QuotaExceeded = "TENANT_QUOTA_EXCEEDED";
            public const string AccessDenied = "TENANT_ACCESS_DENIED";
            public const string SlugAlreadyExists = "TENANT_SLUG_ALREADY_EXISTS";
        }

        public static class Branch
        {
            public const string NotFound = "BRANCH_NOT_FOUND";
            public const string Inactive = "BRANCH_INACTIVE";
            public const string CodeAlreadyExists = "BRANCH_CODE_ALREADY_EXISTS";
        }

        public static class User
        {
            public const string NotFound = "USER_NOT_FOUND";
            public const string EmailAlreadyExists = "USER_EMAIL_ALREADY_EXISTS";
            public const string UsernameAlreadyExists = "USER_USERNAME_ALREADY_EXISTS";
            public const string PhoneAlreadyExists = "USER_PHONE_ALREADY_EXISTS";
            public const string Inactive = "USER_INACTIVE";
        }

        public static class Role
        {
            public const string NotFound = "ROLE_NOT_FOUND";
            public const string NameAlreadyExists = "ROLE_NAME_ALREADY_EXISTS";
        }

        public static class Table
        {
            public const string NotFound = "TABLE_NOT_FOUND";
            public const string CodeAlreadyExists = "TABLE_CODE_ALREADY_EXISTS";
            public const string Occupied = "TABLE_OCCUPIED";
            public const string NotAvailable = "TABLE_NOT_AVAILABLE";
        }

        public static class Menu
        {
            public const string NotFound = "MENU_NOT_FOUND";
            public const string NameAlreadyExists = "MENU_NAME_ALREADY_EXISTS";
            public const string Inactive = "MENU_INACTIVE";
        }

        public static class MenuItem
        {
            public const string NotFound = "MENU_ITEM_NOT_FOUND";
            public const string SkuAlreadyExists = "MENU_ITEM_SKU_ALREADY_EXISTS";
            public const string OutOfStock = "MENU_ITEM_OUT_OF_STOCK";
            public const string Inactive = "MENU_ITEM_INACTIVE";
            public const string PriceInvalid = "MENU_ITEM_PRICE_INVALID";
        }

        public static class Order
        {
            public const string NotFound = "ORDER_NOT_FOUND";
            public const string Empty = "ORDER_EMPTY";
            public const string AlreadyPaid = "ORDER_ALREADY_PAID";
            public const string AlreadyCancelled = "ORDER_ALREADY_CANCELLED";
            public const string CannotCancel = "ORDER_CANNOT_CANCEL";
            public const string InvalidStatusTransition = "ORDER_INVALID_STATUS_TRANSITION";
            public const string Closed = "ORDER_CLOSED";
        }

        public static class Payment
        {
            public const string NotFound = "PAYMENT_NOT_FOUND";
            public const string AlreadyCompleted = "PAYMENT_ALREADY_COMPLETED";
            public const string Failed = "PAYMENT_FAILED";
            public const string MethodNotSupported = "PAYMENT_METHOD_NOT_SUPPORTED";
        }

        public static class Validation
        {
            public const string Required = "FIELD_REQUIRED";
            public const string InvalidFormat = "FIELD_INVALID_FORMAT";
            public const string TooLong = "FIELD_TOO_LONG";
            public const string TooShort = "FIELD_TOO_SHORT";
            public const string InvalidValue = "FIELD_INVALID_VALUE";
            public const string OutOfRange = "FIELD_OUT_OF_RANGE";
        }
    }
}
