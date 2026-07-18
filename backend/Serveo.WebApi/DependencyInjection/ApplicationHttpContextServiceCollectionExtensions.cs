using Serveo.Application.Abstractions;
using Serveo.Domain.Interfaces;
using Serveo.Infrastructure.Authentication;
using Serveo.Infrastructure.Persistence.UnitOfWorks;

namespace Serveo.WebApi.DependencyInjection
{
    internal static class ApplicationHttpContextServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationHttpContext(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddScoped<ISessionContext, SessionContext>();
            services.AddScoped<IRequestContext, HttpRequestContext>();

            return services;
        }
    }
}
