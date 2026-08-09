using DeviceSimulator.API.Devices.Domain.Abstraction;
using DeviceSimulator.API.Devices.Domain.Events;
using System.Text.Json;

namespace DeviceSimulator.API.Devices.Domain.Models
{
	public abstract class Device :Aggregation
	{
		public Guid Id { get; protected set; }

		public string Name { get; protected set; } = default!;

		public DeviceType Type { get; protected set; }

		public DeviceStatus Status { get; protected set; }

		public DeviceState State { get; protected set; }= default!;

		public List<Capability> Capabilities { get; protected set; } = [];

		protected Device()
		{
		}

		protected Device(
		Guid id,
		string name,
		DeviceType type,
		DeviceStatus status,
		DeviceState state,
		List<Capability> capabilities)
		{
			if (id == Guid.Empty)
				throw new ArgumentException(
					"Device id cannot be empty.",
					nameof(id));
			if (string.IsNullOrWhiteSpace(name))
				throw new ArgumentException(
					"Device name cannot be empty.",
					nameof(name));

			if (capabilities is null)
				throw new ArgumentException(
					"Device capabilities cannot be null.",
					nameof(capabilities));

			Id = id;
			Name = name;
			Type = type;
			Status = status;
			State = state;
			Capabilities = capabilities;
		}
		public bool HasCapability(CapabilityType type)
		{
			return Capabilities.Any(x=>x.Type == type);
		}
		public abstract void SetCapability(CapabilityType type, JsonElement value);
		public virtual void SimulateCapability(CapabilityType type, JsonElement value)
		{
			if(!State.HasChanged(type,value)) return;

			SetCapability(type, value);
			AddDomainEvent(new DeviceStateChangedDomainEvent(
				Id,type,value));
		}
	}
}
