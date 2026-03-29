using KayraExport.Microservices.BuildingBlocks.Shared.Domain.Models;
using Microsoft.Extensions.DependencyInjection;

namespace KayraExport.Microservices.BuildingBlocks.Shared.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddJwtAuthentication(this IServiceCollection services, Action<JwtConfig> config)
        {
            JwtConfig jc = new();
            config.Invoke(jc);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jc.Issuer,
                    ValidAudience = jc.Audience,
                    IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(
                        System.Text.Encoding.UTF8.GetBytes(jc.SecurityKey)),
                    ClockSkew = TimeSpan.Zero
                };
            });
        }
    }
}
