using BuildingBlocks.CQRS;
using BuildingBlocks.Exceptions;
using BuildingBlocks.Messaging.Contracts.Commands;
using BuildingBlocks.Messaging.Contracts.Events;
using SmartHome.Application.Data;
using System.Text.Json;

namespace SmartHome.Application.Device.Commands.SetDeviceCapabilityCommand
{
	public record SetDeviceCapabilityCommand(Guid DeviceId, string Type, JsonElement Value) :ICommand<SetDeviceCapabilityResult>;
	public record SetDeviceCapabilityResult(bool IsSuccess);
	public class SetDeviceCapabilityHandler : ICommandHandler<SetDeviceCapabilityCommand, SetDeviceCapabilityResult>
	{
		private readonly ISmartHomeDbContext _context;
		private readonly IIntegrationCommandPublisher _sender;
		public SetDeviceCapabilityHandler(ISmartHomeDbContext context,IIntegrationCommandPublisher sender)
		{
			_context = context;
			_sender=sender;
		}
		public async Task<SetDeviceCapabilityResult> Handle(SetDeviceCapabilityCommand command, CancellationToken cancellationToken)
		{
			var device = await _context.Devices.FindAsync(command.DeviceId, cancellationToken);
			if (device == null) 
				throw new NotFoundException("Device",command.DeviceId);

			if(!device.HasCapability(command.Type))
			{
				return new SetDeviceCapabilityResult(false);
			}

			await SendSetCapabilityIntegrationCommand(command, cancellationToken);
			device.UpdateCapability(command.Type,command.Value);
			await _context.SaveChangesAsync(cancellationToken);

			return new SetDeviceCapabilityResult(true);
			
		}
		private async Task SendSetCapabilityIntegrationCommand (SetDeviceCapabilityCommand command, CancellationToken cancellationToken)
		{
			var integrationCommand = new SetDeviceCapabilityIntegrationCommand(command.DeviceId, command.Type, command.Value);
			await _sender.Send(integrationCommand, cancellationToken);
		}
	}
}
