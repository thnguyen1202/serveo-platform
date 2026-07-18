using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serveo.Application.Abstractions;
using Serveo.Domain.Entities.Identity;
using Serveo.Infrastructure.Persistence.EntityFramework;
using Serveo.Infrastructure.Persistence.Interceptors;
using Serveo.Infrastructure.Persistence.UnitOfWorks;

namespace Serveo.WebApi.DependencyInjection
{
    internal static class ApplicationDbContextServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString =
                configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            services.AddScoped<TrimStringsInterceptor>();
            services.AddScoped<AuditInterceptor>();

            services.AddDbContext<ApplicationDbContext>((sp, options) =>
            {
                //options.UseMySql( //Pomelo.EntityFrameworkCore.MySql
                //    connectionString,
                //    ServerVersion.AutoDetect(connectionString),
                //    mySqlOptions =>
                //    {
                //        mySqlOptions.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
                //        mySqlOptions.EnableRetryOnFailure();
                //    }
                //)

                options.UseSqlServer(connectionString); // Microsoft.EntityFrameworkCore.SqlServer 
                options.AddInterceptors(sp.GetRequiredService<TrimStringsInterceptor>());
                options.AddInterceptors(sp.GetRequiredService<AuditInterceptor>());

#if DEBUG
                options.EnableDetailedErrors();
                options.EnableSensitiveDataLogging();
#endif
            });

            //services.AddScoped<IUserClaimsPrincipalFactory<User>, AppClaimsPrincipalFactory>();
            // Identity (Cookie)
            services.AddIdentity<User, Role>()
                    .AddEntityFrameworkStores<ApplicationDbContext>()
                    .AddDefaultTokenProviders();

            services.AddScoped<IUnitOfWork, UnitOfWork<ApplicationDbContext>>();

            return services;
        }
    }
}
