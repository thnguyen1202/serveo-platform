namespace Serveo.Application
{
    public static class Permissions
    {
        public static class Orders
        {
            public const string View = "orders.view";
            public const string Create = "orders.create";
            public const string Update = "orders.update";
            public const string Cancel = "orders.cancel";
        }

        public static class Products
        {
            public const string View = "products.view";
            public const string Create = "products.create";
            public const string Update = "products.update";
            public const string Delete = "products.delete";
        }

        public static class Users
        {
            public const string View = "users.view";
            public const string Create = "users.create";
            public const string Update = "users.update";
            public const string Delete = "users.delete";
        }

        public static class Roles
        {
            public const string View = "roles.view";
            public const string Create = "roles.create";
            public const string Update = "roles.update";
            public const string Delete = "roles.delete";
        }
    }
}
