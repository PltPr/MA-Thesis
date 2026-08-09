using DeviceSimulator.API.Data;
using DeviceSimulator.API.Devices.Domain.Factories;
using FluentValidation;
using System.Text.Json;

namespace DeviceSimulator.API.Devices.Features.SimulateDeviceCapability
{
	public record SimulateDeviceCapabilityCommand(Guid DeviceId,CapabilityType Type,JsonElement Value) : ICommand<SimulateDeviceCapabilityResult>;
	public record SimulateDeviceCapabilityResult(bool IsSuccess);
	public class SimulateDeviceCapabilityCommandHandler : AbstractValidator<SimulateDeviceCapabilityCommand>
	{
		public SimulateDeviceCapabilityCommandHandler()
		{
			RuleFor(x => x.DeviceId).NotEmpty().WithMessage("DeviceId should not be empty.");
			RuleFor(x => x.Type).IsInEnum().WithMessage("Invalid capability type.");
			RuleFor(x => x.Value).Must(x => x.ValueKind != JsonValueKind.Undefined).WithMessage("Value is required");
		}
	}
	public class SimulateDeviceCapabilityHandler : ICommandHandler<SimulateDeviceCapabilityCommand, SimulateDeviceCapabilityResult>
	{
		private readonly ApplicationDBContext _context;
		private readonly IDeviceFactory _factory;
		public SimulateDeviceCapabilityHandler(ApplicationDBContext context,IDeviceFactory factory)
		{
			_context = context;
			_factory = factory;
		}
		public async Task<SimulateDeviceCapabilityResult> Handle(SimulateDeviceCapabilityCommand command, CancellationToken cancellationToken)
		{
			var deviceEntity = await _context.Devices.FindAsync(command.DeviceId,cancellationToken);
			if(deviceEntity == null) 
				throw new NotFoundException("Device",command.DeviceId);
			var device = _factory.Create(deviceEntity);
			device.SimulateCapability(command.Type, command.Value);

			deviceEntity.State = device.State;

			_context.Entry(deviceEntity)
				.Property(x => x.State)
				.IsModified = true;
			await _context.SaveChangesAsync();

			return new SimulateDeviceCapabilityResult(true);
		}
	}
}
