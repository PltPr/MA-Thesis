using BuildingBlocks.Messaging.Contracts.Commands;
using SmartHome.Application.Device.Commands.SetDeviceCapability;

namespace SmartHome.Application.Device.Commands
{
	public interface IIntegrationCommandPublisher
	{
		Task Send(SetDeviceCapabilityIntegrationCommand command, CancellationToken cancellationToken);
	}
}
