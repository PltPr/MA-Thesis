using System.Text.Json;

namespace DeviceSimulator.API.Devices.Features.SimulateDeviceCapability
{
	public record SimulateDeviceCapabilityRequest(Guid DeviceId,CapabilityType Type, JsonElement Value);
	public record SimulateDeviceCapabilityResponse(bool IsSuccess);
	public class SimulateDeviceCapabilityEndpoint : ICarterModule
	{
		public void AddRoutes(IEndpointRouteBuilder app)
		{
			app.MapPost("/device/simulate", async (SimulateDeviceCapabilityRequest request, ISender sender) =>
			{
				var command = request.Adapt<SimulateDeviceCapabilityCommand>();
				var result = await sender.Send(command);
				var response = result.Adapt<SimulateDeviceCapabilityResponse>();

				return Results.Ok(response);
			})
				.WithName("SimulateCapability");
		}
	}
}
