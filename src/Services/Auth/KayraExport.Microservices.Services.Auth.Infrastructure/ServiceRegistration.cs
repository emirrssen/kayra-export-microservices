using KayraExport.Microservices.BuildingBlocks.Shared.Application.Extensions;
using KayraExport.Microservices.Services.Auth.Application.Helpers;
using KayraExport.Microservices.Services.Auth.Application.Repositories.PostgreSql;
using KayraExport.Microservices.Services.Auth.Application.Services.Abstracts;
using KayraExport.Microservices.Services.Auth.Infrastructure.EntityFrameworkCore;
using KayraExport.Microservices.Services.Auth.Infrastructure.EntityFrameworkCore.Repositories.PostgreSql;
using KayraExport.Microservices.Services.Auth.Infrastructure.Services.Concretes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace KayraExport.Microservices.Services.Auth.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<ITokenService, TokenService>();

            services.AddDbContext<AuthDbContext>(options => options.UseNpgsql(EnvironmentHelper.PostgreSqlConnectionString));
            services.AddScoped<DbContext>(provider => provider.GetRequiredService<AuthDbContext>());
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddSharedCoreServices(x =>
            {
                x.Assembly = typeof(ITokenService).Assembly;
                x.ContextType = typeof(AuthDbContext);
            });
        }
    }
}
