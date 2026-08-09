using DeviceSimulator.API.Devices.Domain.Abstraction;

namespace DeviceSimulator.API.Devices.Features.Events
{
	public class EventDispatcher : IEventDispatcher
	{
		public Task Dispatch(IEnumerable<IDomainEvent> events, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task Dispatch<T>(T integrationEvent, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}
