using DeviceSimulator.API.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDBContext>(options =>
{
	options.UseNpgsql(builder.Configuration.GetConnectionString("Database"));
});

var app = builder.Build();



app.Run();
