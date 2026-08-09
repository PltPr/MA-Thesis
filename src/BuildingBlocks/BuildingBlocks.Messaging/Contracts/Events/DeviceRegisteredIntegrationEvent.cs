using BuildingBlocks.Messaging.Contracts.Models;
using System.Text.Json;

namespace BuildingBlocks.Messaging.Contracts.Events
{
	public record DeviceRegisteredIntegrationEvent(
		Guid DeviceId,
		string Name,
		string Type,
		string Status,
		IReadOnlyDictionary<string,JsonElement> State,
		IReadOnlyCollection<CapabilityIntegrationModel> Capabilities) : IIntegrationEvent;
}
