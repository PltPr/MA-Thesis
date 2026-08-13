using Carter;
using Mapster;
using MediatR;
using SmartHome.Application.Device.Commands.SetDeviceCapabilityCommand;
using System.Text.Json;

namespace SmartHome.API.Endpoints.Device
{
	public record SetDeviceCapabilityRequest(Guid DeviceId,string Type,JsonElement Value);
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

				return Results.Ok(result);
			})
				.WithName("SetDeviceCapability");
		}
	}
}
