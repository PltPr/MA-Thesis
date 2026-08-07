using BuildingBlocks.Exceptions.Handler;
using Carter;
using DeviceSimulator.API.Data;
using DeviceSimulator.API.Devices.Domain.Factories;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using FluentValidation;
using BuildingBlocks.Behaviours;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDBContext>(options =>
{
	options.UseNpgsql(builder.Configuration.GetConnectionString("Database"));
});

builder.Services.AddMediatR(config =>
{
	config.RegisterServicesFromAssembly(typeof(Program).Assembly);
	config.AddOpenBehavior(typeof(ValidationBehavior<,>));
});

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

builder.Services.AddCarter();
builder.Services.ConfigureHttpJsonOptions(options =>
{
	options.SerializerOptions.Converters.Add(
		new JsonStringEnumConverter());
});

builder.Services.AddScoped<IDeviceFactory, DeviceFactory>();

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

var app = builder.Build();

app.UseExceptionHandler(opts => { });

app.MapCarter();

app.Run();
