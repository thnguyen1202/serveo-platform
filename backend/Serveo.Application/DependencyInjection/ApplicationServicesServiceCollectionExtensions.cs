using Microsoft.Extensions.DependencyInjection;
using Serveo.Application.Services;
using System.Reflection;
using System.Runtime.Serialization;

namespace Serveo.Application.DependencyInjection
{
    public static class ApplicationServicesServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            var types = Assembly.GetExecutingAssembly().GetExportedTypes()
                .Where(x => x.IsClass && !x.IsAbstract && !x.IsGenericType && !x.IsNested)
                .Where(x => x.GetInterfaces().Contains(typeof(IService)))
                .ToList();

            foreach (Type item in types)
            {
                foreach (Type interfaces in AllInterfacesForThisType(item))
                {
                    services.Add(new ServiceDescriptor(interfaces, item, ServiceLifetime.Transient));
                }
            }

            return services;
        }

        internal static List<Type> FullyDefinedInterfacesToIgnore { get; set; } = [typeof(IDisposable), typeof(ISerializable)];
        internal static List<Type> NotFullyDefinedInterfacesToIgnore { get; set; } = [typeof(IService)];

        internal static IEnumerable<Type> AllInterfacesForThisType(Type classType)
        {
            return classType.GetTypeInfo().ImplementedInterfaces
                .Where((interfaceType) => interfaceType.IsPublic &&
                !interfaceType.IsNested &&
                !FullyDefinedInterfacesToIgnore.Contains(interfaceType) &&
                NotFullyDefinedInterfacesToIgnore.All((x) => x.Name != interfaceType.Name));
        }
    }
}
