namespace DeviceSimulator.API.Devices.Features.AddDevice
{
	public record AddDeviceResponse(Guid Id);
	public class AddDeviceEndpoint : ICarterModule
	{
		public void AddRoutes(IEndpointRouteBuilder app)
		{
			app.MapPost("/device", async (AddDeviceRequest request, ISender sender) =>
			{
				var result = await sender.Send(request.ToCommand());
				var response = result.Adapt<AddDeviceResponse>();
				return Results.Ok(response);
			})
				.WithName("AddDevice")
				.Produces<AddDeviceResponse>(StatusCodes.Status201Created)
				.ProducesProblem(StatusCodes.Status400BadRequest)
				.WithDescription("Add Device")
				.WithSummary("Add Device");
		}
	}
}
