using BuildingBlocks.Messaging.Contracts.Events;
using BuildingBlocks.Messaging.Contracts.Models;
using DeviceSimulator.API.Data;
using DeviceSimulator.API.Devices.Domain.Factories;
using DeviceSimulator.API.Devices.Features.Events.IntegrationEvents;
using DeviceSimulator.API.Extensions;
using FluentValidation;
using System.Text.Json;

namespace DeviceSimulator.API.Devices.Features.AddDevice
{
	public record AddDeviceCommand(string Name,DeviceType Type,List<Capability>Capabilities ) :ICommand<AddDeviceResult>;
	public record AddDeviceResult(Guid Id);

	public class AddDeviceCommandValidator : AbstractValidator<AddDeviceCommand>
	{
		public AddDeviceCommandValidator()
		{
			RuleFor(x => x.Name).NotEmpty().WithMessage("Name should not be empty.");
			RuleFor(x=>x.Type).IsInEnum().WithMessage("Invalid device type.");
			RuleFor(x=>x.Capabilities).NotEmpty().WithMessage("Device must have at least one capability.");
			RuleForEach(x => x.Capabilities)
				.SetValidator(new CapabilityValidator());
		}
	}
	public class CapabilityValidator : AbstractValidator<Capability>
	{
		public CapabilityValidator()
		{
			RuleFor(x => x.Type).IsInEnum().WithMessage("Invalid capability type.");
			RuleFor(x => x.Range).NotNull().When(x => x.Type == CapabilityType.Brightness).WithMessage($"One or more capabilities required range.");
		}
	}

	public class AddDeviceHandler : ICommandHandler<AddDeviceCommand, AddDeviceResult>
	{
		private readonly ApplicationDBContext _context;
		private readonly IDeviceFactory _deviceFactory;
		private readonly IIntegrationEventPublisher _publisher;
		public AddDeviceHandler(ApplicationDBContext context,IDeviceFactory deviceFactory,IIntegrationEventPublisher publisher)
		{
			_deviceFactory = deviceFactory;
			_context = context;
			_publisher = publisher;
		}


		public async Task<AddDeviceResult> Handle(AddDeviceCommand command, CancellationToken cancellationToken)
		{
			var id = Guid.NewGuid();
			var state = CreateInitializationState(command.Capabilities);

			var device = _deviceFactory.Create(id,command.Name,command.Type,DeviceStatus.Offline,state,command.Capabilities);
			var deviceEntity = device.ToDeviceEntity();

			await _context.Devices.AddAsync(deviceEntity);
			await _context.SaveChangesAsync(cancellationToken);

			await PublishDeviceRegisteredEvent(device,cancellationToken);

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
					CapabilityType.Temperature =>
						JsonSerializer.SerializeToElement(15),
					_ => throw new NotSupportedException(
						$"Unsupported capability {capability.Type}")
				};
			}
			return DeviceState.Of(values);
		}

		private async Task PublishDeviceRegisteredEvent(Device device,CancellationToken cancellationToken)
		{
			var integrationEvent = new DeviceRegisteredIntegrationEvent(
				device.Id,
				device.Name,
				device.Type.ToString(),
				device.Status.ToString(),
				device.State.Values,
				device.Capabilities
					.Select(x => new CapabilityIntegrationModel(
					x.Type.ToString(),
					x.Range is null ? null :
					new ValueRangeIntegrationModel(x.Range.Min, x.Range.Max),x.Options)).ToList());

			await _publisher.Publish(integrationEvent, cancellationToken);
		}
	}
}
