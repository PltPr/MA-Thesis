using BuildingBlocks.Messaging.Contracts.Events;
using MassTransit;
using MediatR;
using SmartHome.Application.Device.Commands.RegisterDevice;
using SmartHome.Domain.Device.Models;

namespace SmartHome.Application.Device.Commands.Consumers.DeviceRegisteredConsumer
{
	public class DeviceRegisteredConsumer : IConsumer<DeviceRegisteredIntegrationEvent>
	{
		private readonly ISender _sender;
		public DeviceRegisteredConsumer(ISender sender)
		{
			_sender = sender;
		}
		public async Task Consume(ConsumeContext<DeviceRegisteredIntegrationEvent> context)
		{
			var command = context.Message;

			await _sender.Send(new RegisterDeviceCommand(
				command.DeviceId, command.Name, command.Type, command.Status, command.State, command.Capabilities), context.CancellationToken);
		}
	}
}
