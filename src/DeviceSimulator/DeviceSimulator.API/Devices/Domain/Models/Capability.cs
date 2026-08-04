namespace DeviceSimulator.API.Devices.Domain.Models
{
	public class Capability
	{
		public CapabilityType Type { get; set; }
		public ValueRange? Range { get; set; }
	}
}
