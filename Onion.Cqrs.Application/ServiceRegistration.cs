using Microsoft.Extensions.DependencyInjection;
using Onion.Cqrs.Application.Interface.Context;
using Onion.Cqrs.Application.Interface;
using System.Reflection;

namespace Onion.Cqrs.Application
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cf => cf.RegisterServicesFromAssembly(typeof(ServiceRegistration).Assembly));

            return services;

        }
    }
}
