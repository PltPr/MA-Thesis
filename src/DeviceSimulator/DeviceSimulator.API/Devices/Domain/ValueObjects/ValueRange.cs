namespace DeviceSimulator.API.Devices.Domain.ValueObjects
{
	public readonly record struct ValueRange
	{
		public int Min { get; }
		public int Max { get; }
	}
}
