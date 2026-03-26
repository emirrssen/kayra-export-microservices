using KayraExport.Microservices.BuildingBlocks.Shared.Application.Mİddlewares;
using Microsoft.AspNetCore.Builder;

namespace KayraExport.Microservices.BuildingBlocks.Shared.Application.Extensions
{
    public static class WebApplicationBuilderExtensions
    {
        public static void UseGlobalErrorHandlerMiddleware(this IApplicationBuilder builder)
            => builder.UseMiddleware<GlobalErrorHandlerMiddleware>();
    }
}
