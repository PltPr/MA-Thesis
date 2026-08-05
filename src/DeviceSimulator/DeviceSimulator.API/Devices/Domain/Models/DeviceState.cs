using System.Text.Json;

namespace DeviceSimulator.API.Devices.Domain.Models
{
	public class DeviceState
	{
		public Dictionary<CapabilityType, JsonElement> Values { get; private set; } = [];

		private DeviceState(Dictionary<CapabilityType, JsonElement> values) 
		{
			Values = values;
		}

		public static DeviceState Of(Dictionary<CapabilityType, JsonElement> values)
		{
			if (values.Count == 0)
				throw new ArgumentException("Device state must contain at least one value.",
					nameof(values));

			return new DeviceState(values);
		}
	}
}
