
using DeviceSimulator.API.Data;

namespace DeviceSimulator.API.Devices.Features.GetDevices
{
	public record GetDevicesResponse(IEnumerable<Device> Devices);
	public class GetDeviceEndpoint : ICarterModule
	{
		public void AddRoutes(IEndpointRouteBuilder app)
		{
			app.MapGet("/device", async (ISender sender) =>
			{
				var result = await sender.Send(new GetDevicesQuery());
				var response = result.Adapt<GetDevicesResponse>();
				return Results.Ok(response);
			})
				.WithName("GetDevices");
		}
	}
}
