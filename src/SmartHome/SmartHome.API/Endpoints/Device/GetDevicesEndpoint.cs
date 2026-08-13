using Carter;
using Mapster;
using MediatR;
using SmartHome.Application.Device.Queries.GetDevices;

namespace SmartHome.API.Endpoints.Device
{
	public record GetDevicesResponse(IEnumerable<DeviceModel> Devices);
	public class GetDevicesEndpoint : ICarterModule
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
