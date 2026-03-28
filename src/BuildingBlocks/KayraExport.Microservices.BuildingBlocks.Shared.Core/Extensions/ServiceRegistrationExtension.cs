using FluentValidation;
using KayraExport.Microservices.BuildingBlocks.Shared.Application.Pipelines;
using KayraExport.Microservices.BuildingBlocks.Shared.Application.Services.Abstract;
using KayraExport.Microservices.BuildingBlocks.Shared.Application.Services.Concrete;
using KayraExport.Microservices.BuildingBlocks.Shared.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace KayraExport.Microservices.BuildingBlocks.Shared.Application.Extensions
{
    public static class ServiceRegistrationExtension
    {
        /// <summary>
        /// Ortak kütüphaneleri çağırıldıkları projelerde ilgili assembly'ye göre register eder.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assembly"></param>
        public static void AddSharedCoreServices(this IServiceCollection services, Action<ServiceRegistrationConfig> config)
        {
            ServiceRegistrationConfig src = new();
            config.Invoke(src);

            // MediatR'ın çağırıldığı projede eklenmesini sağlar.
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(src.Assembly));

            // Fluent validation'ın çağırıldığı projede eklenmesini sağlar.
            services.AddValidatorsFromAssembly(src.Assembly);

            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(FluentValidationPipeline<,>));

            services.AddScoped<ITransactionService, TransactionService>(x =>
            {
                var dbContext = (DbContext)x.GetRequiredService(src.ContextType);
                return new TransactionService(dbContext);
            });
        }
    }
}
