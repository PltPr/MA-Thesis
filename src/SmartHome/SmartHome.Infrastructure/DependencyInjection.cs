using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmartHome.Application.Data;
using SmartHome.Infrastructure.Data;

namespace SmartHome.Infrastructure
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,IConfiguration configuration)
		{
			var connectionString = configuration.GetConnectionString("Database");
			services.AddDbContext<SmartHomeDbContext>(options =>
			{
				options.UseNpgsql(connectionString);
			});

			services.AddScoped<ISmartHomeDbContext, SmartHomeDbContext>();

			return services;
		}
	}
}
