
namespace DeviceSimulator.API.Devices.Features.GetDeviceById
{
	public record GetDeviceByIdResponse(Device Device);
	public class GetDeviceByIdEndpoint : ICarterModule
	{
		public void AddRoutes(IEndpointRouteBuilder app)
		{
			app.MapGet("/device/{id}", async (Guid id, ISender sender) =>
			{
				var result = await sender.Send(new GetDeviceByIdQuery(id));
				var response = result.Adapt<GetDeviceByIdResponse>();

				return Results.Ok(response);
			})
				.WithName("GetDeviceById");
		}
	}
}
