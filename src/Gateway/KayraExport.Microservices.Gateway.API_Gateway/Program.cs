using KayraExport.Microservices.Gateway.API_Gateway;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddServices(builder.Configuration);

var app = builder.Build();

app.UseRateLimiter();
app.MapReverseProxy();
app.Run();