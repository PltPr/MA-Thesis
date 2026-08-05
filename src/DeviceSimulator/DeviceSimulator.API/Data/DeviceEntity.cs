namespace DeviceSimulator.API.Data
{
	public sealed class DeviceEntity
	{
		public Guid Id { get; set; }
		public string Name { get; set; } = default!;
		public DeviceType Type { get; set; }
		public DeviceStatus Status { get; set; }
		public DeviceState State { get; set; } = default!;
		public List<Capability> Capabilities { get; set; } = [];
	}
}
