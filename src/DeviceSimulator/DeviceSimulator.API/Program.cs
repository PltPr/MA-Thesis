using BuildingBlocks.Exceptions.Handler;
using Carter;
using DeviceSimulator.API.Data;
using DeviceSimulator.API.Devices.Domain.Factories;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDBContext>(options =>
{
	options.UseNpgsql(builder.Configuration.GetConnectionString("Database"));
});

builder.Services.AddMediatR(config =>
{
	config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

builder.Services.AddCarter();
builder.Services.ConfigureHttpJsonOptions(options =>
{
	options.SerializerOptions.Converters.Add(
		new JsonStringEnumConverter());
});

builder.Services.AddScoped<IDeviceFactory, DeviceFactory>();

var app = builder.Build();

app.UseExceptionHandler(opts => { });

app.MapCarter();

app.Run();
