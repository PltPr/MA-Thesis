using System.Text.Json;

namespace DeviceSimulator.API.Devices.Domain.Models
{
	public class DeviceState
	{
		public Dictionary<string, JsonElement> Values { get; private set; } = [];

		private DeviceState(Dictionary<string, JsonElement> values) 
		{
			Values = values;
		}

		public static DeviceState Of(Dictionary<string, JsonElement> values)
		{
			if (values.Count == 0)
				throw new ArgumentException("Device state must contain at least one value.",
					nameof(values));

			return new DeviceState(values);
		}

		public void Set<T>(CapabilityType type, T value)
		{
			Values[type.ToString()] = JsonSerializer.SerializeToElement(value);
		}

		public T Get<T>(CapabilityType type)
		{
			if(!Values.TryGetValue(type.ToString(), out var value))
				throw new InvalidOperationException(
					$"Capability {type} does not exist in device state.");

			return value.Deserialize<T>() 
				?? throw new InvalidOperationException(
					$"Cannot deserialize value for {type}.");
		}
	}
}
