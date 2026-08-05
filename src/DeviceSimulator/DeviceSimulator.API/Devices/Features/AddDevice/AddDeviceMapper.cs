namespace DeviceSimulator.API.Devices.Features.AddDevice
{
	public static class AddDeviceMapper
	{
		public static AddDeviceCommand ToCommand(this AddDeviceRequest request)
		{
			return new AddDeviceCommand(
				request.Name,
				request.Type,
				request.Capabilities
					.Select(x => Capability.Of(
						x.Type,
						x.Range is null
							? null
							: ValueRange.Of(x.Range.Min, x.Range.Max)))
					.ToList());
		}
	}
}
