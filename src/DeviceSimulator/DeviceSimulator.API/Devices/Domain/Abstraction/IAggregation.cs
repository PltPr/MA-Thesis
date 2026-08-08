namespace DeviceSimulator.API.Devices.Domain.Abstraction
{
	public interface IAggregation
	{
		IReadOnlyCollection<IDomainEvent> DomainEvents { get; }
		void ClearDomainEvents();
	}
}
