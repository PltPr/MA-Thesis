using SmartHome.Domain.Device.ValueObjects;
using System.Text.Json;

namespace SmartHome.Domain.Device.Models
{
	public class Capability
	{
		public string Type { get; private set; } = null!;
		public ValueRange? Range { get; private set; }
		public List<JsonElement>? Options { get; private set; }

		private Capability()
		{
		}

		private Capability(
			string type,
			ValueRange? range,
			List<JsonElement>? options)
		{
			Type = type;
			Range = range;
			Options = options;
		}

		public static Capability Of(
		string type,
		ValueRange? range,
		List<JsonElement>? options)
		{
			if (string.IsNullOrWhiteSpace(type))
				throw new ArgumentException(
					"Capability type is required.",
					nameof(type));

			return new Capability(
				type,
				range,
				options);
		}
	}
}
