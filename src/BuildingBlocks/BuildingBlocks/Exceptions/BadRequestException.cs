namespace BuildingBlocks.Exceptions
{
	public class BadRequestException :Exception
	{
		public BadRequestException(string message) : base(message) 
		{
			
		}
		public BadRequestException(string message, string description) : base(message)
		{
			Description = description;
		}
		public string? Description { get; }
	}
}
