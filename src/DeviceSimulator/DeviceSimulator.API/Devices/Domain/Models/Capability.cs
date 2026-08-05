namespace DeviceSimulator.API.Devices.Domain.Models
{
	public class Capability
	{
		public CapabilityType Type { get; private set; }
		public ValueRange? Range { get; private set; }

		private Capability(CapabilityType type, ValueRange? range)
		{
			Type = type;
			Range = range;
		}
		private Capability() { }

		public static Capability Of(CapabilityType type, ValueRange? range =null)
		{
			if (type == CapabilityType.Brightness && range is null)
				throw new ArgumentException(
					"Brightness capability requires a value range.");

			return new Capability(type, range);
		}
	}

	
}
