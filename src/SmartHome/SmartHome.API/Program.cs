using SmartHome.API;
using SmartHome.Application;
using SmartHome.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices(builder.Configuration)
	.AddInfrastructureServices(builder.Configuration)
	.AddApiServices();

var app = builder.Build();

app.UseApiServices();

app.Run();
