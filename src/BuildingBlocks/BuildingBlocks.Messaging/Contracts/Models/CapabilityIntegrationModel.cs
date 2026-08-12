using System.Text.Json;

namespace BuildingBlocks.Messaging.Contracts.Models
{
	public record CapabilityIntegrationModel(
		string Type,
		ValueRangeIntegrationModel? Range,
		List<JsonElement>? Options);
}
