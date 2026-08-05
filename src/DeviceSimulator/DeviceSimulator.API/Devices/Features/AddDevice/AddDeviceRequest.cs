namespace DeviceSimulator.API.Devices.Features.AddDevice
{
	public record AddDeviceRequest(
		string Name,
		DeviceType Type,
		List<CapabilityDto> Capabilities);

	public record CapabilityDto(
		CapabilityType Type,
		ValueRangeDto? Range);

	public record ValueRangeDto(
		int Min,
		int Max);
	
}
