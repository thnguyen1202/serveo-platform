namespace Serveo.WebApi.DependencyInjection
{
    public static class ApplicationAuthorizationServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationAuthorization(this IServiceCollection services)
        {
            // 4. Authorization: policy-based để mix nhiều scheme
            //services.AddAuthorization(options =>
            //{
            //    // Policy cho API JWT + ApiKey
            //    options.AddPolicy("ApiPolicy", policy =>
            //        policy.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme,
            //                                        ApiKeyAuthOptions.DefaultScheme)
            //              .RequireAuthenticatedUser());

            //    // Policy cho Basic + JWT + ApiKey (nếu cần)
            //    options.AddPolicy("ApiBasicPolicy", policy =>
            //        policy.AddAuthenticationSchemes(BasicAuthOptions.DefaultScheme,
            //                                        JwtBearerDefaults.AuthenticationScheme,
            //                                        ApiKeyAuthOptions.DefaultScheme)
            //              .RequireAuthenticatedUser());

            //    // Policy cho Web Admin
            //    options.AddPolicy("WebAdminOnly", policy =>
            //        policy.RequireRole("Admin"));
            //});

            services.AddAuthorization();

            return services;
        }
    }
}
