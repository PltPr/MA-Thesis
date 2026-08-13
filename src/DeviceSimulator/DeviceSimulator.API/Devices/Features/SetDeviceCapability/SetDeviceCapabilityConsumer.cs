using BuildingBlocks.Messaging.Contracts.Commands;
using MassTransit;

namespace DeviceSimulator.API.Devices.Features.SetDeviceCapability
{
	public class SetDeviceCapabilityConsumer : IConsumer<SetDeviceCapabilityIntegrationCommand>
	{
		private readonly ISender _sender;
		public SetDeviceCapabilityConsumer(ISender sender)
		{
			_sender = sender;
		}
		public async Task Consume(ConsumeContext<SetDeviceCapabilityIntegrationCommand> context)
		{
			var command = context.Message;
			var type = Enum.Parse<CapabilityType>(command.Type,ignoreCase:true);
			await _sender.Send(new SetDeviceCapabilityCommand(command.DeviceId, type, command.Value),context.CancellationToken);
		}
	}
}
