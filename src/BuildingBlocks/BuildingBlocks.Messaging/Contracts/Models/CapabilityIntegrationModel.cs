namespace BuildingBlocks.Messaging.Contracts.Models
{
	public record CapabilityIntegrationModel(
		string Type,
		ValueRangeIntegrationModel? Range);
}
