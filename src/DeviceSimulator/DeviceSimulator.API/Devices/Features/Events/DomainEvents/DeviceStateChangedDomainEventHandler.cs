using BuildingBlocks.Messaging.Contracts.Events;
using DeviceSimulator.API.Devices.Domain.Events;
using DeviceSimulator.API.Devices.Features.Events.IntegrationEvents;

namespace DeviceSimulator.API.Devices.Features.Events.DomainEvents
{
	public class DeviceStateChangedDomainEventHandler : INotificationHandler<DeviceStateChangedDomainEvent>
	{
		private readonly ILogger<DeviceStateChangedDomainEventHandler> _logger;
		private readonly IIntegrationEventPublisher _publisher;
		public DeviceStateChangedDomainEventHandler(ILogger<DeviceStateChangedDomainEventHandler> logger, IIntegrationEventPublisher publisher)
		{
			_logger = logger;
			_publisher = publisher;
		}
		public async Task Handle(DeviceStateChangedDomainEvent domainEvent, CancellationToken cancellationToken)
		{
			_logger.LogInformation("Domain event handled: {DomainEvent}", domainEvent.GetType().Name);
			var integrationEvent = domainEvent.Adapt<DeviceStateChangedIntegrationEvent>();
			await _publisher.Publish(integrationEvent, cancellationToken);
		}
	}
}
