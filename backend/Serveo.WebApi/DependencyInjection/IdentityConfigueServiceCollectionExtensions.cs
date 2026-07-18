using Microsoft.AspNetCore.Identity;

namespace Serveo.WebApi.DependencyInjection
{
    public static class IdentityConfigueServiceCollectionExtensions
    {
        public static IServiceCollection AddIdentityConfigue(this IServiceCollection services)
        {

            services.Configure<SecurityStampValidatorOptions>(o => o.ValidationInterval = TimeSpan.FromMinutes(30));

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1440);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = false;

                // User settings.
                options.User.AllowedUserNameCharacters =
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;

                // Default SignIn settings.
#if DEBUG
                options.SignIn.RequireConfirmedEmail = false;
#else
                options.SignIn.RequireConfirmedEmail = true;
#endif
                options.SignIn.RequireConfirmedPhoneNumber = false;
            });

            services.Configure<DataProtectionTokenProviderOptions>(o => o.TokenLifespan = TimeSpan.FromHours(3));

            services.Configure<PasswordHasherOptions>(option =>
            {
                option.IterationCount = 12000;
            });

            return services;
        }
    }
}
