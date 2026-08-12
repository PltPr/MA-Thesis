using BuildingBlocks.Behaviours;
using BuildingBlocks.Messaging.MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmartHome.Application.Device.Commands;
using System.Reflection;

namespace SmartHome.Application
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection services,IConfiguration configuration)
		{
			services.AddMediatR(cfg =>
			{
				cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
				cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
			});

			services.AddScoped<IIntegrationCommandPublisher,MassTransitCommandPublisher>();

			services.AddMessageBroker(configuration, Assembly.GetExecutingAssembly());

			return services;
		}
	}
}
