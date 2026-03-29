using KayraExport.Microservices.Services.Log.Core;

var builder = WebApplication.CreateBuilder(args);

builder.AddSerilog();
builder.Services.AddCoreServices();

var app = builder.Build();

app.Run();