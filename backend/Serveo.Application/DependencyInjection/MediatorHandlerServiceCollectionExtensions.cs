using Microsoft.Extensions.DependencyInjection;
using Serveo.Application.Abstractions.Mediator;
using Serveo.Application.Behaviors;
using System.Reflection;

namespace Serveo.Application.DependencyInjection
{
    public static class MediatorHandlerServiceCollectionExtensions
    {
        //public static IServiceCollection AddMediator(this IServiceCollection services)
        //{
        //    var assembly = typeof(LoginHandler).Assembly;

        //    var handlers = assembly
        //        .GetTypes()
        //        .Where(t =>
        //            t.IsClass &&
        //            !t.IsAbstract);

        //    foreach (var handler in handlers)
        //    {
        //        foreach (var service in handler.GetInterfaces())
        //        {
        //            if (!service.IsGenericType)
        //                continue;

        //            var generic = service.GetGenericTypeDefinition();

        //            if (generic == typeof(ICommandHandler<,>))
        //            {
        //                services.AddTransient(service, handler);
        //            }
        //        }
        //    }

        //    services.AddScoped<IMediator, Mediator>();

        //    return services;
        //}
        public static IServiceCollection AddMediatorHandler(this IServiceCollection services)
        {
            services.AddScoped<IMediator, Mediator>();

            var handlerTypes = Assembly.GetExecutingAssembly().GetExportedTypes()
                .Where(t => t.IsClass
                         && !t.IsAbstract
                         && !t.IsGenericTypeDefinition)
                .Select(t => new
                {
                    Implementation = t,
                    Interfaces = t.GetInterfaces()
                        .Where(i => i.IsGenericType
                                 && i.GetGenericTypeDefinition() == typeof(ICommandHandler<,>))
                })
                .Where(x => x.Interfaces.Any())
                .ToList();

            foreach (var handler in handlerTypes)
            {
                foreach (var @interface in handler.Interfaces)
                {
                    services.AddTransient(@interface, handler.Implementation);
                }
            }

            return services;
        }

        //public static IServiceCollection AddMediatorHandler(this IServiceCollection services)
        //{
        //    services.AddScoped<IMediator, Mediator>();

        //    var assembly = Assembly.GetExecutingAssembly();

        //    var handlers = assembly.GetExportedTypes()
        //        .Where(t => t.IsClass
        //            && !t.IsAbstract)
        //        .SelectMany(t => t.GetInterfaces()
        //            .Where(i =>
        //                i.IsGenericType &&
        //                i.GetGenericTypeDefinition() ==
        //                typeof(ICommandHandler<,>))
        //            .Select(i => new
        //            {
        //                Service = i,
        //                Implementation = t
        //            }));

        //    foreach (var handler in handlers)
        //    {
        //        services.AddScoped(
        //            handler.Service,
        //            handler.Implementation);
        //    }

        //    return services;
        //}

        //public static IServiceCollection AddApplicationHandler(this IServiceCollection services)
        //{
        //    var handlerTypes = Assembly.GetExecutingAssembly().GetExportedTypes()
        //        .Where(t => t.IsClass
        //                 && !t.IsAbstract
        //                 && !t.IsGenericTypeDefinition)
        //        .Select(t => new
        //        {
        //            Implementation = t,
        //            Interfaces = t.GetInterfaces()
        //                .Where(i => i.IsGenericType
        //                         && i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>))
        //        })
        //        .Where(x => x.Interfaces.Any())
        //        .ToList();

        //    foreach (var handler in handlerTypes)
        //    {
        //        foreach (var @interface in handler.Interfaces)
        //        {
        //            services.AddTransient(@interface, handler.Implementation);
        //        }
        //    }

        //    return services;
        //}

        //        private static readonly Type[] HandlerInterfaces =
        //{
        //    typeof(IRequestHandler<,>),
        //    typeof(ICommandHandler<,>),
        //    typeof(IQueryHandler<,>)
        //};
        //.Where(i => i.IsGenericType && HandlerInterfaces.Contains(i.GetGenericTypeDefinition()))
    }


}
