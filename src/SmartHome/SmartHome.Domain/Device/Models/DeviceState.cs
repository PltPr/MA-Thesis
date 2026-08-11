using System.Dynamic;
using System.Text.Json;

namespace SmartHome.Domain.Device.Models
{
	public class DeviceState
	{
		private readonly Dictionary<string, JsonElement> _values = [];
		public IReadOnlyDictionary<string, JsonElement> Values => _values;

		private DeviceState() { }

		private DeviceState(IReadOnlyDictionary<string, JsonElement> values)
		{
			_values = new Dictionary<string, JsonElement> (values);
		}
		public static DeviceState Create(
		IReadOnlyDictionary<string, JsonElement> values)
		{
			ArgumentNullException.ThrowIfNull(values);

			return new DeviceState(values);
		}
		public bool HasValue(string capability)
		{
			return _values.ContainsKey(capability);
		}

		public JsonElement Get(string capability)
		{
			if(!_values.TryGetValue(capability, out var value))
				throw new InvalidOperationException(
				$"Capability '{capability}' does not exist in device state.");

			return value;
		}
		public void Set(string capability, JsonElement value)
		{
			_values[capability] = value;
		}
	}
}
