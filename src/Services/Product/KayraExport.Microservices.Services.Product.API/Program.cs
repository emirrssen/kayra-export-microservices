using KayraExport.Microservices.BuildingBlocks.Shared.Application.Extensions;
using KayraExport.Microservices.Services.Product.Application.Helpers;
using KayraExport.Microservices.Services.Product.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddInfrastructureServices();
builder.Services.AddJwtAuthentication(x =>
{
    x.Issuer = EnvironmentHelper.JwtIssuer;
    x.Audience = EnvironmentHelper.JwtAudience;
    x.SecurityKey = EnvironmentHelper.JwtSecurityKey;
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.UseGlobalErrorHandlerMiddleware();

app.Run();