using DeviceSimulator.API.Data;
using System.Xml.Linq;

namespace DeviceSimulator.API.Extensions
{
	public static class DeviceMappers
	{
		public static DeviceEntity ToDeviceEntity(this Device device)
		{
			return new DeviceEntity
			{
				Id = device.Id,
				Name = device.Name,
				Type = device.Type,
				Status = device.Status,
				State = device.State,
				Capabilities = device.Capabilities
			};
		}
	}
}
