using KayraExport.Microservices.Services.Auth.Application;
using KayraExport.Microservices.Services.Auth.Infrastructure;
using KayraExport.Microservices.BuildingBlocks.Shared.Application.Extensions;

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

app.UseGlobalErrorHandlerMiddleware();

app.Run();