using DeviceSimulator.API.Devices.Domain.Events;

namespace DeviceSimulator.API.Devices.Features.Events.DomainEvents
{
	public class DeviceStateChangedDomainEventHandler : INotificationHandler<DeviceStateChangedDomainEvent>
	{
		private readonly ILogger<DeviceStateChangedDomainEventHandler> _logger;
		public DeviceStateChangedDomainEventHandler(ILogger<DeviceStateChangedDomainEventHandler> logger)
		{
			_logger = logger;
		}
		public async Task Handle(DeviceStateChangedDomainEvent domainEvent, CancellationToken cancellationToken)
		{
			_logger.LogInformation("Domain event handled: {DomainEvent}", domainEvent.GetType().Name);
		}
	}
}
