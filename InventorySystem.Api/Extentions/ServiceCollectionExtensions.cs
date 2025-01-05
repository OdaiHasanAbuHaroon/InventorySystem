using System.Reflection;
using InventorySystem.ServiceImplementation;

namespace InventorySystem.Api
{
    /// <summary>
    /// Provides extension methods for registering services in the dependency injection container.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Registers services with the dependency injection container based on their lifetimes.
        /// </summary>
        /// <param name="services">The service collection to which the services will be added.</param>
        /// <param name="assembly">The assembly to scan for service implementations.</param>
        /// <returns>The updated <see cref="IServiceCollection"/> instance.</returns>
        public static IServiceCollection AddLibraryServices(this IServiceCollection services, Assembly assembly)
        {
            var types = assembly.GetTypes();

            // Register Scoped services
            var scopedServices = types.Where(t => typeof(IIScoped).IsAssignableFrom(t) && t.IsClass);
            foreach (var service in scopedServices)
            {
                var interfaces = service.GetInterfaces().Where(i => i != typeof(IIScoped));
                foreach (var @interface in interfaces)
                {
                    services.AddScoped(@interface, service);
                }
            }

            // Register Singleton services
            var singletonServices = types.Where(t => typeof(IISingleton).IsAssignableFrom(t) && t.IsClass);
            foreach (var service in singletonServices)
            {
                var interfaces = service.GetInterfaces().Where(i => i != typeof(IISingleton));
                foreach (var @interface in interfaces)
                {
                    services.AddSingleton(@interface, service);
                }
            }

            // Register Transient services
            var transientServices = types.Where(t => typeof(IITransient).IsAssignableFrom(t) && t.IsClass);
            foreach (var service in transientServices)
            {
                var interfaces = service.GetInterfaces().Where(i => i != typeof(IITransient));
                foreach (var @interface in interfaces)
                {
                    services.AddTransient(@interface, service);
                }
            }

            return services;
        }
    }
}
