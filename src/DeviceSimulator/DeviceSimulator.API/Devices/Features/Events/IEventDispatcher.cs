using DeviceSimulator.API.Devices.Domain.Abstraction;

namespace DeviceSimulator.API.Devices.Features.Events
{
	public interface IEventDispatcher
	{
		Task Dispatch(
			IEnumerable<IDomainEvent> events,
			CancellationToken cancellationToken);

		Task Dispatch<T>(T integrationEvent,
			CancellationToken cancellationToken);
	}
}
