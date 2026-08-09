using System.Text.Json;

namespace BuildingBlocks.Messaging.Contracts.Events
{
	public record DeviceStateChangedIntegrationEvent(
		Guid DeviceId,
		string Capability,
		JsonElement Value) :IIntegrationEvent;
}
