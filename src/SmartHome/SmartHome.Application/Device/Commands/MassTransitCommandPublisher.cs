using BuildingBlocks.Messaging.Contracts.Commands;
using MassTransit;
using SmartHome.Application.Device.Commands.SetDeviceCapability;

namespace SmartHome.Application.Device.Commands
{
	public class MassTransitCommandPublisher : IIntegrationCommandPublisher
	{
		private readonly ISendEndpointProvider _sendEndpointProvider;
		public MassTransitCommandPublisher(ISendEndpointProvider sendEndpointProvider)
		{
			_sendEndpointProvider = sendEndpointProvider;
		}

		public async Task Send(SetDeviceCapabilityIntegrationCommand command, CancellationToken cancellationToken) 
		{
			var endpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:set-device-capability"));
			await endpoint.Send(command, cancellationToken);
		}
	}
}
