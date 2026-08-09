using BuildingBlocks.Messaging.Contracts.Events;

namespace DeviceSimulator.API.Devices.Features.Events.IntegrationEvents
{
	public interface IIntegrationEventPublisher
	{
		Task Publish<T>(T integrationEvent,
			CancellationToken cancellationToken)
			where T : IIntegrationEvent;
	}
}
