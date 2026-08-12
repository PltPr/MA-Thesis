
using BuildingBlocks.CQRS;
using BuildingBlocks.Messaging.Contracts.Models;
using SmartHome.Application.Data;
using SmartHome.Domain.Device.Models;
using SmartHome.Domain.Device.ValueObjects;
using System.Text.Json;

namespace SmartHome.Application.Device.Commands.RegisterDevice
{
	public record RegisterDeviceCommand
		(Guid Id, string Name, string Type, string Status, IReadOnlyDictionary<string, JsonElement> State, IReadOnlyCollection<CapabilityIntegrationModel> Capabilities) :ICommand<RegisterDeviceResult>;
	public record RegisterDeviceResult(Guid Id);
	public class RegisterDeviceHandler : ICommandHandler<RegisterDeviceCommand, RegisterDeviceResult>
	{
		private readonly ISmartHomeDbContext _context;
		public RegisterDeviceHandler(ISmartHomeDbContext context)
		{
			_context = context;
		}
		public async Task<RegisterDeviceResult> Handle(RegisterDeviceCommand command, CancellationToken cancellationToken)
		{
			var state = DeviceState.Of(command.State);

			var capabilities = command.Capabilities
				.Select(x=>Capability.Of(x.Type,x.Range is null ? null : ValueRange.Of(x.Range.Min, x.Range.Max), x.Options)).ToList();

			var device = SmartHome.Domain.Device.Models.Device.Create(command.Id,command.Name,command.Type,command.Status,state,capabilities);

			_context.Devices.Add(device);

			await _context.SaveChangesAsync(cancellationToken);
			return new RegisterDeviceResult(device.Id);

		}
	}
}
