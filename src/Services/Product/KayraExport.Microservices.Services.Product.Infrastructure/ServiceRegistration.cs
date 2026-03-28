using KayraExport.Microservices.BuildingBlocks.Shared.Application.Extensions;
using KayraExport.Microservices.Services.Product.Application.Helpers;
using KayraExport.Microservices.Services.Product.Application.Repositories.PostgreSql;
using KayraExport.Microservices.Services.Product.Infrastructure.EntityFrameworkCore;
using KayraExport.Microservices.Services.Product.Infrastructure.EntityFrameworkCore.Repositories.PostgreSql;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace KayraExport.Microservices.Services.Product.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddDbContext<ProductDbContext>(options => options.UseNpgsql(EnvironmentHelper.PostgreSqlConnectionString));
            services.AddScoped<DbContext>(provider => provider.GetRequiredService<ProductDbContext>());

            services.AddScoped<IProductRepository, ProductRepository>();

            services.AddSharedCoreServices(x =>
            {
                x.Assembly = typeof(IProductRepository).Assembly;
                x.ContextType = typeof(ProductDbContext);
            });
        }
    }
}
