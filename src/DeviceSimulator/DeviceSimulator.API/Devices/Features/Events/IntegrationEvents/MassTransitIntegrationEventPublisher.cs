using BuildingBlocks.Messaging.Contracts.Events;
using MassTransit;

namespace DeviceSimulator.API.Devices.Features.Events.IntegrationEvents
{
	public class MassTransitIntegrationEventPublisher : IIntegrationEventPublisher
	{
		private readonly IPublishEndpoint _publishEndpoint;
		public MassTransitIntegrationEventPublisher(IPublishEndpoint publishEndpoint)
		{
			_publishEndpoint = publishEndpoint;
		}

		public Task Publish<T>(T integrationEvent, CancellationToken cancellationToken) where T : IIntegrationEvent
		{
			return _publishEndpoint.Publish(integrationEvent, cancellationToken);
		}
	}
}
