namespace DeviceSimulator.API.Devices.Domain.ValueObjects
{
	public record ValueRange
	{
		public int Min { get; init; }
		public int Max { get; init; }

		private ValueRange(int min,int max)
		{
			Min= min; 
			Max = max;
		}

		public static ValueRange Of(int min,int max)
		{
			if (min > max)
				throw new ArgumentException("\"Max\" value cannot be smaller than \"Min\"");

			return new ValueRange(min,max);
		}
	}
	
}
