using KayraExport.Microservices.BuildingBlocks.Shared.Application.Extensions;
using KayraExport.Microservices.BuildingBlocks.Shared.Domain.Consts;
using KayraExport.Microservices.Services.Auth.Application;
using KayraExport.Microservices.Services.Auth.Infrastructure;
using Rebus.Bus;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddInfrastructureServices();
builder.Services.AddApplicationServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthorization();

app.MapControllers();

app.UseGlobalErrorHandlerMiddleware(LogServiceNameConst.AuthService);

app.Run();