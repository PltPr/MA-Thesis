using DeviceSimulator.API.Data;
using DeviceSimulator.API.Devices.Domain.Factories;
using DeviceSimulator.API.Extensions;
using System.Text.Json;

namespace DeviceSimulator.API.Devices.Features.AddDevice
{
	public record AddDeviceCommand(string Name,DeviceType Type,List<Capability>Capabilities ) :ICommand<AddDeviceResult>;
	public record AddDeviceResult(Guid Id);

	public class AddDeviceHandler : ICommandHandler<AddDeviceCommand, AddDeviceResult>
	{
		private readonly ApplicationDBContext _context;
		private readonly IDeviceFactory _deviceFactory;
		public AddDeviceHandler(ApplicationDBContext context,IDeviceFactory deviceFactory)
		{
			_deviceFactory = deviceFactory;
			_context = context;
		}


		public async Task<AddDeviceResult> Handle(AddDeviceCommand command, CancellationToken cancellationToken)
		{
			var id = Guid.NewGuid();
			var state = CreateInitializationState(command.Capabilities);

			var device = _deviceFactory.Create(id,command.Name,command.Type,DeviceStatus.Offline,state,command.Capabilities);
			var deviceEntity = device.ToDeviceEntity();

			await _context.Devices.AddAsync(deviceEntity);
			await _context.SaveChangesAsync(cancellationToken);

			return new AddDeviceResult(id);
		}


		private static DeviceState CreateInitializationState(List<Capability> capabilities)
		{
			var values = new Dictionary<string, JsonElement>();

			foreach (var capability in capabilities )
			{
				values[capability.Type.ToString()] = capability.Type switch
				{
					CapabilityType.Power =>
						JsonSerializer.SerializeToElement(false),
					CapabilityType.Brightness =>
						JsonSerializer.SerializeToElement(capability.Range!.Min),
					CapabilityType.Position =>
						JsonSerializer.SerializeToElement(capability.Range!.Min),
					_ => throw new NotSupportedException(
						$"Unsupported capability {capability.Type}")
				};
			}
			return DeviceState.Of(values);
		}
	}
}
