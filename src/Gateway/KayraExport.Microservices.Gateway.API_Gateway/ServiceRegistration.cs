using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;

namespace KayraExport.Microservices.Gateway.API_Gateway
{
    public static class ServiceRegistration
    {
        public static void AddServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddRateLimiter(options =>
            {
                options.AddFixedWindowLimiter("fixed-5-per-second", opt =>
                {
                    opt.PermitLimit = 5;
                    opt.Window = TimeSpan.FromSeconds(1);
                    opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                    opt.QueueLimit = 0;
                });

                options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
            });

            services.AddReverseProxy()
                .LoadFromConfig(config.GetSection("ReverseProxy"));
        }
    }
}