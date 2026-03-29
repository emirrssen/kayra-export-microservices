using KayraExport.Microservices.BuildingBlocks.Shared.Application.Middlewares;
using KayraExport.Microservices.BuildingBlocks.Shared.Domain.Consts;
using Microsoft.AspNetCore.Builder;
using Rebus.Bus;

namespace KayraExport.Microservices.BuildingBlocks.Shared.Application.Extensions
{
    public static class WebApplicationBuilderExtensions
    {
        public static void UseGlobalErrorHandlerMiddleware(this IApplicationBuilder builder, string serviceName)
            => builder.UseMiddleware<GlobalErrorHandlerMiddleware>(serviceName);
    }
}
