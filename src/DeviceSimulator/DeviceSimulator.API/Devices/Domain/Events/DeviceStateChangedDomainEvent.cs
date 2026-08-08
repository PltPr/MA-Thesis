using DeviceSimulator.API.Devices.Domain.Abstraction;
using System.Text.Json;

namespace DeviceSimulator.API.Devices.Domain.Events
{
	public record DeviceStateChangedDomainEvent(
		Guid DeviceId,
		CapabilityType Type,
		JsonElement Value) :IDomainEvent;
	
}
