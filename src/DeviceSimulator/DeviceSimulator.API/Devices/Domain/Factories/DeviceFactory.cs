using DeviceSimulator.API.Data;

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
					DimmableLight.Create(id, name, status, state, capabilities),

				DeviceType.Blind =>
					Light.Create(id, name, status, state, capabilities),

				_ => throw new NotSupportedException(
					$"Unsupported device type: {type}")
			};
		}

		public Device Create(DeviceEntity entity)
		{
			return Create(
				entity.Id,
				entity.Name,
				entity.Type,
				entity.Status,
				entity.State,
				entity.Capabilities);
		}
	}
}
