
using DeviceSimulator.API.Data;
using DeviceSimulator.API.Devices.Domain.Factories;
using FluentValidation;
using System.Text.Json;

namespace DeviceSimulator.API.Devices.Features.SetDeviceState
{
	public record SetDeviceCapabilityCommand(Guid DeviceId, CapabilityType Type, JsonElement Value) : ICommand<SetDeviceCapabilityResult>;
	public record SetDeviceCapabilityResult(bool IsSuccess);

	public class SetDeviceCapabilityCommandValidator :AbstractValidator<SetDeviceCapabilityCommand>
	{
		public SetDeviceCapabilityCommandValidator()
		{
			RuleFor(x => x.DeviceId).NotEmpty().WithMessage("DeviceId should not be empty.");
			RuleFor(x => x.Type).IsInEnum().WithMessage("Invalid capability type.");
			RuleFor(x => x.Value).Must(x => x.ValueKind != JsonValueKind.Undefined).WithMessage("Value is required");
		}
	}
	public class SetDeviceCapabilityHandler : ICommandHandler<SetDeviceCapabilityCommand, SetDeviceCapabilityResult>
	{
		private readonly ApplicationDBContext _context;
		private readonly IDeviceFactory _factory;
		public SetDeviceCapabilityHandler(ApplicationDBContext context, IDeviceFactory factory)
		{
			_context = context;
			_factory = factory;
		}
		public async Task<SetDeviceCapabilityResult> Handle(SetDeviceCapabilityCommand command, CancellationToken cancellationToken)
		{
			var deviceEntity = await _context.Devices.FindAsync([command.DeviceId],cancellationToken);
			if (deviceEntity == null) throw new KeyNotFoundException($"Device not found, {command.DeviceId}");

			var device = _factory.Create(deviceEntity);
			device.SetCapability(command.Type, command.Value);
			deviceEntity.State=device.State;

			_context.Entry(deviceEntity.State)
				.Property(x => x.Values)
				.IsModified = true;

			await _context.SaveChangesAsync(cancellationToken);
			return new SetDeviceCapabilityResult(true);
		}
	}
}
