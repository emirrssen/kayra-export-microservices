using KayraExport.Microservices.Services.Auth.Application.Services.Abstracts;
using KayraExport.Microservices.Services.Auth.Infrastructure.Services.Concretes;
using Microsoft.Extensions.DependencyInjection;

namespace KayraExport.Microservices.Services.Auth.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureIntegrationServices(this IServiceCollection services)
        {
            services.AddScoped<ITokenService, TokenService>();
        }
    }
}
