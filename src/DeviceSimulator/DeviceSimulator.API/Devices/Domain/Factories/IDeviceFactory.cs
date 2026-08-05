namespace DeviceSimulator.API.Devices.Domain.Factories
{
	public interface IDeviceFactory
	{
		public Device Create(
			Guid id,
			string name,
			DeviceType type,
			DeviceStatus status,
			DeviceState state,
			List<Capability> capabilities);
	}
}
