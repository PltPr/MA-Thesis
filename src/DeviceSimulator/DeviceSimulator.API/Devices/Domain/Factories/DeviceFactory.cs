namespace DeviceSimulator.API.Devices.Domain.Factories
{
	public class DeviceFactory :IDeviceFactory
	{
		public Device Create(
			Guid id,
			string name,
			DeviceType type,
			DeviceStatus status,
			DeviceState state,
			List<Capability> capabilities)
		{
			return type switch
			{
				DeviceType.Light =>
					Light.Create(id, name, status, state, capabilities),

				DeviceType.DimmableLight =>
					Light.Create(id, name, status, state, capabilities),

				DeviceType.Blind =>
					Light.Create(id, name, status, state, capabilities),

				_ => throw new NotSupportedException(
					$"Unsupported device type: {type}")
			};
		}
	}
}
