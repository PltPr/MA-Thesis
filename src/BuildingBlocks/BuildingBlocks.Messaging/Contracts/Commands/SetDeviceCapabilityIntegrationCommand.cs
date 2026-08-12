using BuildingBlocks.Messaging.Contracts.Events;
using System.Text.Json;

namespace BuildingBlocks.Messaging.Contracts.Commands
{
	public record SetDeviceCapabilityIntegrationCommand(
		Guid DeviceId,
		string Type,
		JsonElement Value);

}
