namespace SmartHome.Domain.Device.ValueObjects
{
	public class ValueRange
	{
		public int Min { get; private set; }
		public int Max { get; private set; }

		private ValueRange(int min,int max )
		{
			if (min > max)
				throw new ArgumentException(
					"Minimum cannot be greater than maximum.");

			Min = min; 
			Max = max;
		}

		public static ValueRange Of(int min,int max)
		{
			return new ValueRange(min,max);
		}
	}
}
