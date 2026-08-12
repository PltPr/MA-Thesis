using BuildingBlocks.Messaging.Contracts.Commands;

namespace SmartHome.Application.Device.Commands
{
	public interface IIntegrationCommandPublisher
	{
		Task Send(SetDeviceCapabilityIntegrationCommand command, CancellationToken cancellationToken);
	}
}
