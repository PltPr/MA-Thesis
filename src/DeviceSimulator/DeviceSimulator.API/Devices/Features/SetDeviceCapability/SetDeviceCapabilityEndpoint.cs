using DeviceSimulator.API.Devices.Features.SetDeviceState;
using System.Text.Json;

namespace DeviceSimulator.API.Devices.Features.SetDeviceCapability
{
	public record SetDeviceCapabilityRequest(Guid DeviceId, CapabilityType Type, JsonElement Value);
	public record SetDeviceCapabilityResponse(bool IsSuccess);
	public class SetDeviceCapabilityEndpoint : ICarterModule
	{
		public void AddRoutes(IEndpointRouteBuilder app)
		{
			app.MapPost("/device/capability", async (SetDeviceCapabilityRequest request, ISender sender) =>
			{
				var command = request.Adapt<SetDeviceCapabilityCommand>();
				var result = await sender.Send(command);
				var response = result.Adapt<SetDeviceCapabilityResponse>();

				return Results.Ok(response);
			})
				.WithName("SetDeviceCapability");
		}
	}
}
