using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace KayraExport.Microservices.BuildingBlocks.Shared.Core.Extensions
{
    public static class ServiceRegistrationExtension
    {
        /// <summary>
        /// Ortak kütüphaneleri çağırıldıkları projelerde ilgili assembly'ye göre register eder.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assembly"></param>
        public static void AddSharedCoreServices(this IServiceCollection services, Assembly assembly)
        {
            // MediatR'ın çağırıldığı projede eklenmesini sağlar.
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));

            // Fluent validation'ın çağırıldığı projede eklenmesini sağlar.
            services.AddValidatorsFromAssembly(assembly);
        }
    }
}
