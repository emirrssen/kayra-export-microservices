using KayraExport.Microservices.BuildingBlocks.Shared.Application.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace KayraExport.Microservices.Services.Auth.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddSharedCoreServices(Assembly.GetExecutingAssembly());
        }
    }
}
